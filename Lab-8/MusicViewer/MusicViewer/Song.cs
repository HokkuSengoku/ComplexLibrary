namespace MusicViewer;

using System;

public class Song
{
    public Song(int id, string title, TimeSpan duration)
    {
        Id = id;
        Title = title;
        Duration = duration;
    }

    public int Id { get; }

    public string Title { get; }

    public TimeSpan Duration { get; }
}