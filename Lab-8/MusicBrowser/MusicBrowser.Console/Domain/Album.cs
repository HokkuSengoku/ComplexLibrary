namespace MusicBrowser.Console.Domain
{
    using System;

    public sealed class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }
    }
}
