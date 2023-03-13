namespace Autocomplete.Basic
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public delegate void HintedControlEventHandler(HintedControl sender);

    public sealed class HintedControl
    {
        private readonly object _lockObject = new object();
        private readonly StringBuilder _text = new StringBuilder();
        private string _hint = string.Empty;
        private int _lastWordLength;

        public event HintedControlEventHandler TypingEvent;

        public string Hint
        {
            get => _hint;

            set
            {
                if (value != Hint)
                {
                    SetHint(value);
                }
            }
        }

        public string Text
        {
            get => _text.ToString();

            set
            {
                if (value != Text)
                {
                    SetText(value);
                }
            }
        }

        public string LastWord
        {
            get => _text.ToString(_text.Length - _lastWordLength, _lastWordLength);

            set
            {
                if (value != LastWord)
                {
                    SetLastWord(value);
                }
            }
        }

        public void Run()
        {
            while (ProcessInput())
            {
                TypingEvent?.Invoke(this);
            }
        }

        private void DetectLastWord()
        {
            var match = Regex.Match(_text.ToString(), @"\w+$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            _lastWordLength = match.Length;
        }

        private void SetText(string value)
        {
            _text.Clear();
            _text.Append(value);

            DetectLastWord();
            Display();
        }

        private void SetHint(string value)
        {
            _hint = value;

            Display();
        }

        private void SetLastWord(string value)
        {
            if (_lastWordLength > 0)
            {
                _text.Remove(Math.Max(0, _text.Length - _lastWordLength), _lastWordLength);
            }

            _lastWordLength = value.Length;
            _text.Append(value);

            Display();
        }

        private void Display()
        {
            lock (_lockObject)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(_text);

                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                Console.SetCursorPosition(
                    Math.Max(
                        0,
                        Math.Min(left - _lastWordLength, Console.BufferWidth - (_hint ?? string.Empty).Length)),
                    top + 1);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(_hint);

                Console.SetCursorPosition(left, top);
            }
        }

        private bool ProcessInput()
        {
            var key = Console.ReadKey(false);

            if (char.IsControl(key.KeyChar))
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        LastWord = Hint;
                        return true;

                    case ConsoleKey.Enter:
                        _text.AppendLine();
                        break;

                    case ConsoleKey.Backspace:
                        if (_text.Length > 0)
                        {
                            _text.Length--;
                        }

                        break;

                    case ConsoleKey.Escape:
                        return false;

                    default:
                        _text.Clear();
                        break;
                }
            }
            else
            {
                _text.Append(key.KeyChar);
            }

            DetectLastWord();
            Display();

            return true;
        }
    }
}