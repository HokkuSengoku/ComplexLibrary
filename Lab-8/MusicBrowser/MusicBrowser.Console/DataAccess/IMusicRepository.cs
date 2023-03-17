namespace MusicBrowser.Console.DataAccess
{
    using System.Collections.Generic;
    using MusicBrowser.Console.Domain;

    public interface IMusicRepository
    {
        IEnumerable<Album> ListAlbums();

        IEnumerable<Song> ListSongs(Album album);

        void Delete(Song song);

        void Delete(Album album);

        Song Add(Song song);

        Album Add(Album album);
    }
}