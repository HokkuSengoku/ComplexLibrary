namespace MusicBrowser.Console.Application
{
    using System;
    using System.Text;
    using MusicBrowser.Console.Domain;

    public sealed class ExpandableList
    {
        private readonly StringBuilder _buffer = new StringBuilder();
        private readonly int _bufferWidth = Console.BufferWidth;
        private readonly int _dataHeight;
        private readonly string _ellipsisLine;

        private readonly MusicListModel _listModel;

        private int _offset;
        private int _selectedIndex;
        private int _totalCount;

        public ExpandableList(MusicListModel listModel)
        {
            _listModel = listModel;

            _buffer.Append(' ');
            while (_buffer.Length <= _bufferWidth - 2)
            {
                _buffer.Append("^ ");
            }

            _buffer.Append(' ', _bufferWidth - _buffer.Length);

            _ellipsisLine = _buffer.ToString();
            _buffer.Clear();

            _offset = 0;
            _selectedIndex = -1;

            if (listModel != null)
            {
                listModel.ChangeEvent += ChangeHandle;
            }

            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            _dataHeight = Console.BufferHeight - 2;

            ChangeHandle();
        }

        public object SelectedItem
        {
            get
            {
                var index = SelectedIndex;

                foreach (var album in _listModel.Albums)
                {
                    if (index-- == 0)
                    {
                        return album;
                    }

                    foreach (var song in _listModel.ListSongs(album))
                    {
                        if (index-- == 0)
                        {
                            return song;
                        }
                    }
                }

                return null;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                if (value <= -1)
                {
                    _selectedIndex = -1;
                    _offset = 0;
                }
                else
                {
                    _selectedIndex = (value >= _totalCount) ? _totalCount - 1 : value;

                    _offset = _offset > _selectedIndex
                                 ? _selectedIndex
                                 : (_offset <= _selectedIndex - _dataHeight ? _selectedIndex - _dataHeight + 1 : _offset);
                }

                Draw();
            }
        }

        public void ChangeHandle()
        {
            _totalCount = _listModel.Albums.Count;

            foreach (var album in _listModel.Albums)
            {
                _totalCount += _listModel.ListSongs(album).Count;
            }

            _selectedIndex = _selectedIndex >= _totalCount ? _totalCount - 1 : _selectedIndex;

            Draw();
        }

        public void Run()
        {
            ChangeHandle();

            while (true)
            {
                var keyInfo = Console.ReadKey();
                var item = SelectedItem;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.DownArrow:
                        SelectedIndex++;
                        break;

                    case ConsoleKey.PageDown:
                        SelectedIndex += _dataHeight;
                        break;

                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                        }

                        break;

                    case ConsoleKey.PageUp:
                        SelectedIndex = SelectedIndex > _dataHeight ? SelectedIndex - _dataHeight : 0;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Enter:
                        if (item is Album selectedAlbum)
                        {
                            _listModel.CollapseAll();
                            if (keyInfo.Key != ConsoleKey.LeftArrow)
                            {
                                _listModel.ExpandAlbum(selectedAlbum);
                            }
                        }

                        break;

                    case ConsoleKey.Delete:
                        if (item is Album album)
                        {
                            _listModel.Delete(album);
                        }

                        if (item is Song)
                        {
                            _listModel.Delete(item as Song);
                        }

                        break;

                    case ConsoleKey.A:
                        var albumData = ConsoleDialog.CreateAlbum();
                        if (albumData != null)
                        {
                            _listModel.AddAlbum(albumData.Item1, albumData.Item2);
                            SelectedIndex = _totalCount - 1;
                        }
                        else
                        {
                            _listModel.CollapseAll();
                        }

                        break;

                    case ConsoleKey.S:
                        if (item != null)
                        {
                            var songData = ConsoleDialog.CreateSong();
                            if (songData != null)
                            {
                                _listModel.AddSong(
                                    songData.Item1, songData.Item2, item is Song song ? song.Album : item as Album);
                            }
                            else
                            {
                                _listModel.CollapseAll();
                            }
                        }

                        break;
                }

                // We have to clear unused input from the console buffer
                Console.Write("\b \b");
            }
        }

        private void Draw()
        {
            var bufferHeight = Console.BufferHeight;
            var contentHeight = bufferHeight - 2;

            // One empty line at the top
            _buffer.Clear();
            if (_offset > 0)
            {
                AddEllipsis();
            }
            else
            {
                AddSpace();
            }

            // Albums and songs
            var lineIndex = 0;
            var tail = false;
            foreach (var album in _listModel.Albums)
            {
                if (lineIndex++ >= _offset)
                {
                    if (lineIndex > _offset + contentHeight)
                    {
                        tail = true;
                        break;
                    }

                    DrawAlbum(album);
                }

                var order = 0;
                foreach (var song in _listModel.ListSongs(album))
                {
                    if (lineIndex++ >= _offset)
                    {
                        if (lineIndex > _offset + contentHeight)
                        {
                            tail = true;
                            break;
                        }

                        DrawSong(song, ++order);
                    }
                }
            }

            // Trailing lines
            while (_buffer.Length < bufferHeight * _bufferWidth)
            {
                if (tail)
                {
                    AddEllipsis();
                }
                else
                {
                    AddSpace();
                }
            }

            // For slow console, it could be less flicker to draw everything first
            Console.CursorVisible = false;
            _buffer.Length = (bufferHeight * _bufferWidth) - 1;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write(_buffer.ToString());

            // And than, redraw selected item in is's highlighted state
            if (_selectedIndex >= _offset && _selectedIndex < _offset + contentHeight)
            {
                var bufferIndex = _selectedIndex - _offset + 1;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, bufferIndex);
                Console.Write(_buffer.ToString(bufferIndex * _bufferWidth, _bufferWidth));
            }

            // This is the safe cursof position, preventing the scrolling
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
        }

        private void AddEllipsis()
        {
            _buffer.Append(_ellipsisLine);
        }

        private void AddSpace()
        {
            _buffer.Append(' ', _bufferWidth);
        }

        private void DrawAlbum(Album album)
        {
            var displayTitle = album.Title.Length < (_bufferWidth - 10) ? album.Title : album.Title.Substring(0, _bufferWidth - 10);
            _buffer.Append(
                string.Format(
                    "{0} {1,-" + (_bufferWidth - 3) + "} ",
                    _listModel.IsExpanded(album) ? '-' : '+',
                    displayTitle + "(" + album.Date.Year + ")"));
        }

        private void DrawSong(Song song, int order)
        {
            var displayTitle = song.Title.Length < (_bufferWidth - 20) ? song.Title : song.Title.Substring(0, _bufferWidth - 20);
            _buffer.Append(
                string.Format(
                "{0,5}.{1,-" + (_bufferWidth - 17) + "} {2:c} s",
                order,
                displayTitle,
                song.Duration));
        }
    }
}
