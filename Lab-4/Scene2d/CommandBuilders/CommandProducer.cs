namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class CommandProducer : ICommandBuilder
    {
        private static readonly Dictionary<Regex, Func<ICommandBuilder>> Commands =
            new Dictionary<Regex, Func<ICommandBuilder>>
            {
                { new Regex("^add rectangle .*"), () => new AddRectangleCommandBuilder() },
                /* declare more builders here */
            };

        private ICommandBuilder _currentBuilder;

        public bool IsCommandReady
        {
            get
            {
                if (_currentBuilder == null)
                {
                    return false;
                }

                return _currentBuilder.IsCommandReady;
            }
        }

        public void AppendLine(string line)
        {
            if (_currentBuilder == null)
            {
                foreach (var pair in Commands)
                {
                    if (pair.Key.IsMatch(line))
                    {
                        _currentBuilder = pair.Value();
                        break;
                    }
                }

                if (_currentBuilder == null)
                {
                    throw new BadFormatException();
                }
            }

            _currentBuilder.AppendLine(line);
        }

        public ICommand GetCommand()
        {
            if (_currentBuilder == null)
            {
                return null;
            }

            var command = _currentBuilder.GetCommand();
            _currentBuilder = null;

            return command;
        }
    }
}
