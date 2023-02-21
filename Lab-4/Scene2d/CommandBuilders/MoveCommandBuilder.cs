namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class MoveCommandBuilder : ICommandBuilder
    {
        private static readonly Regex FigureRegex = new Regex(@"((move)\s(\((\w+||[-])*\))\s(\([+-]?\d*,\s?[+-]?\d*\)))");
        private static readonly Regex SceneRegex = new Regex(@"((move)\s(\((scene\))\s(\([+-]?\d*,\s?[+-]?\d*\))))");
        private static readonly Regex NotNameScene = new Regex(@"(\(scene\))");
        private string _name;
        private ScenePoint _vector;
        private bool _shapeOrScene;

        public bool IsCommandReady
        {
            get
            {
                return true;
            }
        }

        public void AppendLine(string line)
        {
            var separators = new char[] { ' ', '(', ')', ',' };

            if (FigureRegex.Match(line).Success && !NotNameScene.Match(line).Success)
            {
                var match = FigureRegex.Match(line);
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                _name = command[1];
                var coordinates = GetCoordinates(command);
                _vector = new ScenePoint { X = coordinates[0], Y = coordinates[1] };
                _shapeOrScene = true;
            }
            else if (SceneRegex.Match(line).Success)
            {
                var match = SceneRegex.Match(line);
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var coordinates = GetCoordinates(command);
                _vector = new ScenePoint { X = coordinates[0], Y = coordinates[1] };
                _shapeOrScene = false;
            }
            else
            {
                throw new BadFormatException("Error in line 29/37: bad format");
            }
        }

        public double[] GetCoordinates(string[] command)
        {
            var coordinates = new double[2];
            coordinates[0] = Convert.ToDouble(command[2]);
            coordinates[1] = Convert.ToDouble(command[3]);

            return coordinates;
        }

        public ICommand GetCommand()
        {
            if (_shapeOrScene != true)
            {
                return new MoveCommand(_vector, _shapeOrScene);
            }
            else
            {
                return new MoveCommand(_name, _vector, _shapeOrScene);
            }
        }
    }
}