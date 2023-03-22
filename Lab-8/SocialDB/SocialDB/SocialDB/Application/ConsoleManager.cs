namespace SocialDB.Application;
using SocialDB.Domain;
using SocialDB.DataAccess.EntityFramework;

public class ConsoleManager
{
    private readonly EntityFrameworkSocialRepository _repo;
    

    public ConsoleManager( EntityFrameworkSocialRepository repository)
    {
        _repo = repository;  // создаем репозиторий
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Press A to add a user");
            Console.WriteLine("Press B to add a friend");
            Console.WriteLine("Press C to add a message");
            Console.WriteLine("Press F to add a like");
            Console.WriteLine("Press D to delete a user/friend/message/like");
            Console.WriteLine("Press Enter to show users/friends/messages/likes");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.A:
                    CreateUser();
                    break;
                case ConsoleKey.B:
                    CreateFriend();
                    break;
                case ConsoleKey.C:
                    CreateMessage();
                    break;
                case ConsoleKey.F:
                    CreateLike();
                    break;
                default:
                    Console.WriteLine("Invalid key pressed.");
                    break;
            }
        }
    }

    private void CreateUser()
    {
        Console.WriteLine("Enter user name: ");
        string name = Console.ReadLine();

        Console.Write("Enter date of birth (yyyy-mm-dd): ");
        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter gender (0 - female, 1 - male, 2 - other): ");
        int gender = int.Parse(Console.ReadLine());

        User user = new User
        {
            Name = name,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            LastVisit = DateTime.Now,
            Online = false
        };

        _repo.AddUser(user);

        Console.WriteLine($"User {name} added with ID {user.UserId}");
    }

    private void CreateFriend()
    {
        Console.WriteLine("Enter from user ID: ");
        int fromUserId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter to user ID: ");
        int toUserId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter status (0 - pending, 1 - accepted, 2 - rejected): ");
        int status = int.Parse(Console.ReadLine());

        Friend friend = new Friend
        {
            FromUserId = fromUserId,
            ToUserId = toUserId,
            SendDate = DateTime.Now,
            Status = status
        };

        _repo.AddFriend(friend);

        Console.WriteLine($"Friend request from user {fromUserId} to user {toUserId} added with ID {friend.FriendId}");
    }

    private void CreateMessage()
    {
        Console.WriteLine("Enter author ID: ");
        int authorId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter text: ");
        string text = Console.ReadLine();

        Message message = new Message
        {
            AuthorId = authorId,
            SendDate = DateTime.Now,
            Text = text
        };

        _repo.AddMessage(message);

        Console.WriteLine($"Message added with ID {message.MessageId}");
    }
    private void CreateLike()
    { Console.WriteLine("Введите ID пользователя, которому хотите поставить лайк:");
        int userId = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите ID сообщения, которому хотите поставить лайк:");
        int messageId = int.Parse(Console.ReadLine());
        
            Like like = new Like
            {
                UserId = userId,
                MessageId = messageId
            };

            _repo.AddLike(like);

            Console.WriteLine("Лайк успешно добавлен!");
    }
    
}
