namespace MusicBrowser.Console.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MusicBrowser.Console.DataAccess;
    using MusicBrowser.Console.Domain;

    public sealed class MusicListModel
    {
        private readonly IMusicRepository _musicRepository;
        private readonly List<Album> _albums = new List<Album>();

        private readonly IDictionary<int, bool> _albumExpanded = new Dictionary<int, bool>();
        private readonly IDictionary<int, IList<Song>> _albumSongs = new Dictionary<int, IList<Song>>();

        public MusicListModel(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;

            ReadAlbumsList();
        }

        public event Action ChangeEvent;

        public IList<Album> Albums => _albums;

        public void CollapseAll()
        {
            foreach (var album in _albums)
            {
                _albumExpanded[album.Id] = false;
                _albumSongs[album.Id].Clear();
            }

            Changed();
        }

        public void ExpandAlbum(Album album)
        {
            IList<Song> songs = _musicRepository.ListSongs(album).ToList();

            _albumSongs[album.Id] = songs;
            _albumExpanded[album.Id] = true;

            Changed();
        }

        public void ReadAlbumsList()
        {
            _albums.Clear();

            foreach (var album in _musicRepository.ListAlbums())
            {
                PutAlbumToModel(album);
            }

            Changed();
        }

        public void Delete(Song song)
        {
            var albumId = song.Album.Id;

            _musicRepository.Delete(song);

            _albumSongs[albumId].Remove(song);

            Changed();
        }

        public void Delete(Album album)
        {
            var albumId = album.Id;

            _musicRepository.Delete(album);

            _albums.Remove(album);
            _albumSongs.Remove(albumId);
            _albumExpanded.Remove(albumId);

            Changed();
        }

        public void AddSong(string songTitle, TimeSpan songDuration,  Album album)
        {
            if (album == null)
            {
                return;
            }

            Song song = new Song
            {
                Duration = songDuration,
                Album = album,
                Title = songTitle,
            };

            _albumSongs[album.Id].Add(_musicRepository.Add(song));

            Changed();
        }

        public void AddAlbum(string albumTitle, DateTime albumDate)
        {
            Album album = _musicRepository.Add(new Album { Date = albumDate, Title = albumTitle });

            PutAlbumToModel(album);

            Changed();
        }

        public IList<Song> ListSongs(Album album)
        {
            return _albumSongs[album.Id];
        }

        public bool IsExpanded(Album album)
        {
            return _albumExpanded[album.Id];
        }

        private void PutAlbumToModel(Album album)
        {
            _albums.Add(album);
            _albumSongs[album.Id] = new List<Song>();
            _albumExpanded[album.Id] = false;
        }

        private void Changed()
        {
            ChangeEvent?.Invoke();
        }
    }
}
