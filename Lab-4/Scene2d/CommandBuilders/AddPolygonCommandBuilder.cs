namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(@"((add\spolygon)\s((\w+||[-])*))|((\s(add\spoint\s\([+-]?\d*,\s?[+-]?\d*\))))|(end\spolygon)");
        private static readonly Regex AddPointRegex = new Regex(@"((add\spoint\s\([+-]?\d*,\s?[+-]?\d*\)))");
        private static readonly Regex CommandRegex = new Regex(@"((add\spolygon)\s((\w+||[-])*))");
        private static readonly Regex EndPolygonRegex = new Regex(@"(end\spolygon)");

        /* Should be set in AppendLine method */
        private IFigure _polygon;

        /* Should be set in AppendLine method */
        private string _name;
        private bool _isCommonReady;
        private List<double> _allCoordinates = new List<double>();

        public bool IsCommandReady
        {
            get
            {
                return _isCommonReady;
            }
        }

        public void AppendLine(string line)
        {
            var separators = new char[] { ' ', '(', ')', ',' };
            var match = RecognizeRegex.Match(line);
            _isCommonReady = false;
            if (match.Value != string.Empty)
            {
                var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (CommandRegex.Match(match.Value).Success)
                {
                    _name = command[2];
                    _isCommonReady = false;
                }

                if (AddPointRegex.Match(match.Value).Success)
                {
                    var coordinates = GetCoordinates(command);
                    foreach (var item in coordinates)
                    {
                        _allCoordinates.Add(item);
                    }

                    _isCommonReady = false;
                }

                if (EndPolygonRegex.Match(match.Value).Success)
                {
                    ScenePoint[] points = new ScenePoint[_allCoordinates.Count / 2];
                    List<ScenePoint> pointss = new List<ScenePoint>();
                    for (var i = 0; i < _allCoordinates.Count; i += 2)
                    {
                        pointss.Add(new ScenePoint { X = _allCoordinates[i], Y = _allCoordinates[i + 1] });
                    }

                    for (var i = 0; i < points.Length; i++)
                    {
                        points[i] = pointss[i];
                    }

                    _polygon = default;
                    _polygon = new PolygonFigure(points);
                    _isCommonReady = true;
                }
            }
            else
            {
                throw new BadFormatException("Error in line 32: bad format");
            }
        }

        public double[] GetCoordinates(string[] command)
        {
            var coordinates = new double[command.Length / 2];
            for (var i = 2; i < command.Length; i++)
            {
                if (double.TryParse(command[i], out coordinates[i - 2]))
                {
                }
            }

            return coordinates;
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);
    }
}