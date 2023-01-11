namespace Scene2d.CommandBuilders
{
    using System;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"((add\spolygon)\s((\w+||[-])*)\n(\s(add\spoint\s\([+-]?\d*,\s?[+-]?\d*\))\n){3,}end\spolygon)");

        /* Should be set in AppendLine method */
        private IFigure _polygon;

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
            var match = RecognizeRegex.Match(@line);
            if (match.Value != string.Empty)
            {
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var coordinates = GetCoordinates(command);
                ScenePoint[] points = new ScenePoint[coordinates.Length / 2];
                _name = command[2];
                _polygon = default;

                for (var i = 0; i < points.Length; i++)
                {
                    points[i] = new ScenePoint { X = coordinates[i], Y = coordinates[i + 1] };
                }

                _polygon = new PolygonFigure(points);
            }
            else
            {
                throw new BadFormatException("Error in line 32: bad format");
            }
        }

        public double[] GetCoordinates(string[] command)
        {
            var coordinates = new double[(command.Length - 5) / 2];
            for (var i = 0; i < command.Length; i++)
            {
                if (double.TryParse(command[i], out coordinates[i]))
                {
                }
            }

            return coordinates;
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);
    }
}