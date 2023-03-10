namespace Social
{
    using System;

    internal class Program
    {
        // TODO: пути к файлам
        private const string PathDirectory = "Data";
        private const string PathUsers = PathDirectory + "/users.json";
        private const string PathFriends = PathDirectory + "/friends.json";
        private const string PathMessages = PathDirectory + "/messages.json";

        private static void Main(string[] args)
        {
           // if (args.Length == 0)
           // {
          //      Console.WriteLine("Имя пользователя не задано!");
           //     return;
          //  }
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                // TODO
                return;
            }

            var socialDataSource = new SocialDataSource(PathUsers, PathFriends, PathMessages);

            var userContext = socialDataSource.GetUserContext(name);

            // todo: вывод в консоль
            PrintUserInfo(userContext);
        }

        private static void PrintUserInfo(UserContext user)
        {
            string gender;
            if (user.User.Gender == 0)
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }

            Console.WriteLine($"Hello {user.User.Name}!\n Your age: {(int)(DateTime.Now - user.User.DateOfBirth).TotalDays / 365} \n Gender: {gender}");
        }
    }
}
