namespace Social
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    internal class Program
    {
        // TODO: пути к файлам
        private const string PathDirectory = "Data";
        private const string PathUsers = PathDirectory + "/users1.json";
        private const string PathFriends = PathDirectory + "/friends1.json";
        private const string PathMessages = PathDirectory + "/messages1.json";

        private static void Main(string[] args)
        {
            bool argsOrUserInput = args.Length > 0;
            string name = string.Empty;
            if (!argsOrUserInput)
            {
                name = Console.ReadLine();
            }
            else
            {
                name = args[0];
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }

            var socialDataSource = new SocialDataSource(PathUsers, PathFriends, PathMessages);

            var userContext = socialDataSource.GetUserContext(name);

            // todo: вывод в консоль
            PrintUserInfo(userContext);
        }

        private static void PrintUserInfo(UserContext user)
        {
            var friends = user.Friends;
            var onlineFriends = user.OnlineFriends;
            var friendshipOffers = user.FriendshipOffers;
            var news = user.News;
            var subscribers = user.Subscribers;

            string gender;
            if (user.User.Gender == 0)
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine("======================================================================================================================================");
            Console.WriteLine($"Hello {user.User.Name}!\nYour age: {(int)(DateTime.Now - user.User.DateOfBirth).TotalDays / 365} \nGender: {gender}");
            Console.WriteLine("===========================================================Your Friends===============================================================");
            Console.WriteLine($"You have {user.Friends.Count} friends:");
            foreach (var friend in friends)
            {
                Console.WriteLine($"{friend.Name}");
            }

            Console.WriteLine("===========================================================Online Friends=============================================================");
            Console.WriteLine($"You have {user.OnlineFriends.Count} Online friends:");
            foreach (var friend in onlineFriends)
            {
                Console.WriteLine($"{friend.Name}");
            }

            Console.WriteLine("=============================================================Subscribers=============================================================");
            Console.WriteLine($"You have {user.Subscribers.Count} subscribers:");
            foreach (var subscriber in subscribers)
            {
                Console.WriteLine($"{subscriber.Name}");
            }

            Console.WriteLine("===========================================================Friend Requests===========================================================");
            Console.WriteLine($"You have {user.FriendshipOffers.Count} new friend requests since your last visit:");
            foreach (var offer in friendshipOffers)
            {
                Console.WriteLine($"{offer.Name}");
            }

            Console.WriteLine("======================================================Updates in the news feed:======================================================");
            foreach (var nws in news)
            {
                Console.WriteLine($"[{nws.AuthorName}]: {nws.Text} \t {nws.Likes.Sum()} likes");
            }

            Console.WriteLine("======================================================================================================================================");
        }
    }
}
