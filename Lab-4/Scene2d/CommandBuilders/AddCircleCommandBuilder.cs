namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddCircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"(((add\scircle))\s((\w+||[-])*)\s((\([+-]?\d*,\s?[+-]?\d*\)))\s(radius)\s[+-]?\d*)");

        /* Should be set in AppendLine method */
        private IFigure _circle;

        /* Should be set in AppendLine method */
        private string _name;

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
            var match = RecognizeRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var coordinates = new double[2];
            double radius;
            if (match.Value != string.Empty && command.Length == 7)
            {
                _name = command[2];
                coordinates = GetCoordinates(command);
                if (Convert.ToDouble(command[6]) <= 0)
                {
                    throw new BadCircleRadiusException("Error in line 38: bad circle radius");
                }
                else
                {
                    radius = Convert.ToDouble(command[6]);
                }

                _circle = new CircleFigure(new ScenePoint { X = coordinates[0], Y = coordinates[1] }, radius);
            }
            else
            {
                throw new BadFormatException("Error in line 49: bad format");
            }
        }

        public double[] GetCoordinates(string[] command)
        {
            var coordinates = new double[2];
            coordinates[0] = Convert.ToDouble(command[3]);
            coordinates[1] = Convert.ToDouble(command[4]);

            return coordinates;
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _circle);
    }
}