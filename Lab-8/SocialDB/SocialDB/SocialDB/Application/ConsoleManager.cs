namespace SocialDB.Application;
using SocialDB.Domain;
using SocialDB.DataAccess.EntityFramework;

public class ConsoleManager
{
    private readonly EntityFrameworkSocialRepository _repo;
    

    public ConsoleManager( EntityFrameworkSocialRepository repository)
    {
        _repo = repository;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("input add/show/delete user/s to add/show/delete a user");
            Console.WriteLine("input add/show/delete friend/s to add/show/delete a friend");
            Console.WriteLine("input add/show/delete message/s to add/show/delete a message");
            Console.WriteLine("input add/show/delete like/s to add/show/delete a like");

            var input = Console.ReadLine();

            switch (input)
            {
                case "add user":
                    CreateUser();
                    break;
                case "add friend":
                    CreateFriend();
                    break;
                case "add message":
                    CreateMessage();
                    break;
                case "add like":
                    CreateLike();
                    break;
                case "show users":
                    ShowUsers();
                    break;
                case "show friends":
                    ShowFriends();
                    break;
                case "show likes":
                    ShowLikes();
                    break;
                case "show messages":
                    ShowMessages();
                    break;
                case "delete user":
                    var x = Console.ReadLine();
                    DeleteUser(x);
                    break;
                case "delete friend":
                    var idFriend = Console.ReadLine();
                    DeleteFriend(idFriend);
                    break;
                case "delete message":
                    var idMessage = Console.ReadLine();
                    DeleteMessage(idMessage);
                    break;
                case "delete like":
                    var messageId = Console.ReadLine();
                    DeleteLike(messageId);
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
    
    private void ShowUsers()
    {
        var x = _repo.GetUsers();

        foreach (var item in x)
        {
            Console.WriteLine($"Id = {item.UserId}\tName = {item.Name}\tDate of Birth = {item.DateOfBirth}\tGender = {item.Gender}\tOnline = {item.Online}");
        }
    }

    private void DeleteUser(string name)
    {
        User userToDelete = _repo.GetUsers().Where(x => x.Name == name).SingleOrDefault();
        _repo.DeleteUser(userToDelete);
    }

    private void CreateFriend()
    {
        Console.WriteLine("Enter from user ID: ");
        int fromUserId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter to user ID: ");
        int toUserId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter status (0 - directed, 1 - viewed, 2 - accepted, 3 - rejected): ");
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

    private void ShowFriends()
    {
        var x = _repo.GetFriends();

        foreach (var item in x)
        {
            Console.WriteLine($"FriendId = {item.FriendId}\tFromId = {item.FromUserId}\tToId = {item.ToUserId}\tSendDate = {item.SendDate}\tStatus = {item.Status}");
        }
    }

    private void DeleteFriend(string id)
    {
        var x = Convert.ToInt32(id);

        Friend friendToDelete = _repo.GetFriends().Where(fId => fId.FriendId == x).SingleOrDefault();
        _repo.DeleteFriend(friendToDelete);
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

    private void ShowMessages()
    {
        var x = _repo.GetMessages();

        foreach (var item in x)
        {
            Console.WriteLine($"MessageId = {item.MessageId}\tAuthorId = {item.AuthorId}\tMessageText = {item.Text}\tSendDate = {item.SendDate}");
        }
    }

    private void DeleteMessage(string idMessage)
    {
        var id = Convert.ToInt32(idMessage);
        
        Message messageToDelete = _repo.GetMessages().Where(mId => mId.MessageId == id).SingleOrDefault();
        _repo.DeleteMessage(messageToDelete);
    }
    private void CreateLike()
    { 
        Console.WriteLine("Введите ID пользователя, которому хотите поставить лайк:");
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

    private void ShowLikes()
    {
        var x = _repo.GetLikes();

        foreach (var item in x)
        {
            Console.WriteLine($"Like from {item.UserId} on message {item.MessageId}");
        }
    }

    private void DeleteLike(string idMessage)
    {
        var id = Convert.ToInt32(idMessage);
        
        Like likeToDelete = _repo.GetLikes().Where(x => x.UserId == id).SingleOrDefault();
        _repo.DeleteLike(likeToDelete);
    }
}
