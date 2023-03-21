namespace MusicBrowser.Console.DataAccess.EntityFramework;

using System.Collections.Generic;
using System.Linq;
using MusicBrowser.Console.Domain;

public class EntityFrameworkMusicRepository : IMusicRepository
{
    private readonly DataContext _dataContext;

    public EntityFrameworkMusicRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<Album> ListAlbums()
    {
        return _dataContext.Albums;
    }

    public IEnumerable<Song> ListSongs(Album album)
    {
        return _dataContext.Songs.Where(x => x.Album.Id == album.Id);
    }

    public void Delete(Song song)
    {
        _dataContext.Songs.Remove(song);
        _dataContext.SaveChanges();
    }

    public void Delete(Album album)
    {
        _dataContext.Albums.Remove(album);
        _dataContext.SaveChanges();
    }

    public Song Add(Song song)
    {
        var result = _dataContext.Songs.Add(song);
        _dataContext.SaveChanges();

        return result.Entity;
    }

    public Album Add(Album album)
    {
        var result = _dataContext.Albums.Add(album);
        _dataContext.SaveChanges();

        return result.Entity;
    }
}