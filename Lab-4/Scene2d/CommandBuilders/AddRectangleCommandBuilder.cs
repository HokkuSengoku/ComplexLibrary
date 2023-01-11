namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"(((add\srectangle))\s((\w+||[-])*)\s(\([+-]?\d*,\s?[+-]?\d*\)\s\(\d*,\s?\d*\)))");

        /* Should be set in AppendLine method */
        private IFigure _rectangle;

        /* Should be set in AppendLine method */
        private string _name;

        public bool IsCommandReady
        {
            get
            {
                /* "add rectangle" is a one-line command so it is always ready */
                return true;
            }
        }

        public void AppendLine(string line)
        {
            var separators = new char[] { ' ', '(', ')', ',' };
            var match = RecognizeRegex.Match(line);
            var coordinates = new double[4];
            if (match.Value != string.Empty)
            {
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                _name = command[2];
                coordinates = GetCoordinates(command);
                _rectangle = new RectangleFigure(new ScenePoint { X = coordinates[0], Y = coordinates[1] }, new ScenePoint { X = coordinates[2], Y = coordinates[3] });
            }
            else
            {
                throw new BadFormatException("Error in line 31: bad format");
            }
        }

        public double[] GetCoordinates(string[] command)
        {
            var coordinates = new double[4];
            coordinates[0] = Convert.ToDouble(command[3]);
            coordinates[1] = Convert.ToDouble(command[4]);
            coordinates[2] = Convert.ToDouble(command[5]);
            coordinates[3] = Convert.ToDouble(command[6]);

            return coordinates;
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _rectangle);
    }
}