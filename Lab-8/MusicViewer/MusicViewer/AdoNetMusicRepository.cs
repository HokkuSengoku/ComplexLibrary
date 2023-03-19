namespace MusicViewer;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class AdoNetMusicRepository : IMusicRepository
{
    private readonly string _connectionString;

    public AdoNetMusicRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Album> ListAlbums()
    {
        IList<Album> results = new List<Album>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var command = new SqlCommand("SELECT * FROM [albums]", connection);

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var albumId = (int)dataReader["albumId"];

                    results.Add(new Album(
                        albumId,
                        (DateTime)dataReader["date"],
                        (string)dataReader["title"],
                        ListAlbumSongs(connection, albumId)));
                }
            }
        }

        return results;
    }

    private IList<Song> ListAlbumSongs(SqlConnection connection, int albumId)
    {
        IList<Song> results = new List<Song>();

        var command = new SqlCommand("SELECT * FROM [songs] WHERE albumId = @AlbumId", connection);
        command.Parameters.AddWithValue("AlbumId", albumId); 

        using (var dataReader = command.ExecuteReader())
        {
            while (dataReader.Read())
            {
                results.Add(new Song(
                    (int)dataReader["albumId"],
                    (string)dataReader["title"],
                    (TimeSpan)dataReader["duration"]));
            }
        }

        return results;
    }
}