namespace MusicBrowser.Console.Application
{
    using System;

    public static class ConsoleDialog
    {
        public static Tuple<string, DateTime> CreateAlbum()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Please, enter the title of new album:");
            var title = Console.ReadLine();
            Console.WriteLine("Please, enter the release date of this album:");
            try
            {
                var date = Convert.ToDateTime(Console.ReadLine());
                return new Tuple<string, DateTime>(title, date);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static Tuple<string, TimeSpan> CreateSong()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Please, enter the title of new song:");
            var title = Console.ReadLine();
            Console.WriteLine("Please, enter the duration of this song in seconds:");
            try
            {
                var seconds = Convert.ToInt32(Console.ReadLine());
                return new Tuple<string, TimeSpan>(title, new TimeSpan(0, 0, seconds));
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
