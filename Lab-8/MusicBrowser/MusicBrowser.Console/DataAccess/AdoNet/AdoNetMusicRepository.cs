namespace MusicBrowser.Console.DataAccess.AdoNet
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using MusicBrowser.Console.Domain;

    public class AdoNetMusicRepository : IMusicRepository
    {
        private readonly string _connectionString;

        public AdoNetMusicRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Album> ListAlbums()
        {
            var result = new List<Album>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [albums]", connection);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(
                            new Album
                            {
                                Id = (int)dataReader["albumId"],
                                Title = (string)dataReader["title"],
                                Date = (DateTime)dataReader["date"],
                            });
                    }
                }
            }

            return result;
        }

        public Album Add(Album album)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    @"
        INSERT INTO dbo.albums (date, title) VALUES (@Date, @Title)
        SELECT SCOPE_IDENTITY()", connection);
                command.Parameters.AddWithValue("@Title", album.Title);
                command.Parameters.AddWithValue("@Date", album.Date);

                var albumId = Convert.ToInt32(command.ExecuteScalar());

                return new Album
                {
                    Id = albumId,
                    Title = album.Title,
                    Date = album.Date,
                };
            }
        }

        public void Delete(Album album)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM [albums] WHERE albumId = @AlbumId",
                    connection);
                command.Parameters.AddWithValue("@AlbumId", album.Id);
                command.ExecuteNonQuery(); // <4>
            }
        }

        public IEnumerable<Song> ListSongs(Album album)
        {
            var result = new List<Song>();
            var albumId = album.Id;

            //// TODO Implement loading songs by album id.
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("select * from songs where songs.albumId = @AlbumId", connection);
                command.Parameters.AddWithValue("@AlbumId", albumId);
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(new Song()
                        {
                            Id = (int)dataReader["songId"],
                            Title = (string)dataReader["title"],
                            Duration = (TimeSpan)dataReader["duration"],
                            Album = album,
                        });
                    }
                }
            }

            return result;
        }

        public void Delete(Song song)
        {
            // TODO Delete song by id
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM [songs] WHERE songId = @SongId",
                    connection);
                command.Parameters.AddWithValue("@SongId", song.Id);
                command.ExecuteNonQuery(); // <4>
            }
        }

        public Song Add(Song song)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    @"
        INSERT INTO dbo.songs (albumId, title, duration) VALUES (@AlbumId, @Title, @Duration)
        SELECT SCOPE_IDENTITY()
        ", connection);
                command.Parameters.AddWithValue("@Duration", song.Duration);
                command.Parameters.AddWithValue("@Title", song.Title);
                command.Parameters.AddWithValue("@AlbumId", song.Album.Id);

                var songId = Convert.ToInt32(command.ExecuteScalar());

                return new Song
                {
                    Id = songId,
                    Title = song.Title,
                    Duration = song.Duration,
                    Album = song.Album,
                };
            }
        }
    }
}