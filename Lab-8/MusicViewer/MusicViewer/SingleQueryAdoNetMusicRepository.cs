using System.Data.SqlClient;

namespace MusicViewer;

public class SingleQueryAdoNetMusicRepository : IMusicRepository
{
    private readonly string _connectionString;
    private List<Song> _songs;

    public SingleQueryAdoNetMusicRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IEnumerable<Album> ListAlbums()
    {
        IList<Album> results = new List<Album>();
        List<Song> songs = new List<Song>();
        Dictionary<int, List<Song>> song = new Dictionary<int, List<Song>>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var albumIdd = 0;
            var command = new SqlCommand("SELECT albums.albumId album_id, albums.date, albums.title albums_title, songs.songId, songs.albumId songsAlbum_id, songs.title song_title, songs.duration from albums JOIN songs on albums.albumId = songs.albumId", connection);

            using (var dataReader = command.ExecuteReader())
            {
                var check = 1;
                while (dataReader.Read())
                {
                    var albumId = (int)dataReader["album_id"];
                    
                        var x = new
                        {
                            id = (int)dataReader["songId"],
                            songTitle = (string)dataReader["song_title"],
                            songDuration = (TimeSpan)dataReader["duration"],
                            albumId = (int)dataReader["songsAlbum_id"]
                        };
                        if (albumId == x.albumId)
                        {
                            if (!song.ContainsKey(albumId))
                            {
                                song[albumId] = new List<Song>
                                {
                                    new Song(x.id, x.songTitle, x.songDuration)
                                };
                            }
                            else
                            {
                                song[albumId].Add(new Song(x.id, x.songTitle, x.songDuration));
                            }
                        }

                        if (albumIdd != albumId)
                    {
                        results.Add(new Album(
                            albumId,
                            (DateTime)dataReader["date"],
                            (string)dataReader["albums_title"], song[albumId]));
                        albumIdd = albumId;
                    }
                }
            }
        }

        return results;
    }
}