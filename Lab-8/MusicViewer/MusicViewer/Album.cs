namespace MusicViewer;

public class Album
{
    private readonly IList<Song> _songs;

    public Album(int id, DateTime date, string title, IList<Song> songs)
    {
        Id = id;
        Date = date;
        Title = title;

        _songs = songs;
    }

    public int Id { get; }

    public DateTime Date { get; }

    public string Title { get; }

    public IEnumerable<Song> Songs => _songs;

}