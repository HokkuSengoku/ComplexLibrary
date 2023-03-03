namespace Social
{
    using System;

    internal class Program
    {
        // TODO: пути к файлам
        private const string PathDirectory = "Data";
        private const string PathUsers = PathDirectory + "";
        private const string PathFriends = PathDirectory + "";
        private const string PathMessages = PathDirectory + "";

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Имя пользователя не задано!");
                return;
            }

            var name = args[0];

            if (string.IsNullOrEmpty(name))
            {
                // TODO
                return;
            }

            var socialDataSource = new SocialDataSource(PathUsers, PathFriends, PathMessages);

            var userContext = socialDataSource.GetUserContext(name);

            // todo: вывод в консоль
        }
    }
}
