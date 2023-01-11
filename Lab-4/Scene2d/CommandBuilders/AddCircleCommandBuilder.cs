namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddCircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"(((add\scircle))\s((\w+||[-])*)\s((\([+-]?\d*,\s?[+-]?\d*\)))\s(radius)\s\d*)");

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
            var coordinates = new double[2];
            double radius;
            if (match.Value != string.Empty)
            {
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                _name = command[2];
                coordinates = GetCoordinates(command);
                radius = Convert.ToDouble(command[6]);
                _circle = new CircleFigure(new ScenePoint { X = coordinates[0], Y = coordinates[1] }, radius);
            }
            else
            {
                throw new BadFormatException("Error in line 27: bad format");
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