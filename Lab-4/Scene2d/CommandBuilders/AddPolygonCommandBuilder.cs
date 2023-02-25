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
                    bool intersect = false;
                    ScenePoint[] points = new ScenePoint[_allCoordinates.Count / 2];
                    List<ScenePoint> pointss = new List<ScenePoint>();
                    for (var i = 0; i < _allCoordinates.Count; i += 2)
                    {
                        pointss.Add(new ScenePoint { X = _allCoordinates[i], Y = _allCoordinates[i + 1] });
                    }

                    HashSet<ScenePoint> polygonPoint = new HashSet<ScenePoint>();
                    for (var i = 0; i < points.Length; i++)
                    {
                        points[i] = pointss[i];
                        polygonPoint.Add(pointss[i]);
                    }

              // intersect = IsIntersected(points[0], points[1], points[points.Length - 1], points[0]);
               //     for (var i = 0; i < points.Length - 3; i++)
               //     {
                //        intersect = IsIntersected(points[i], points[i + 1], points[i + 1], points[i + 2]);
               //     }
                    if (points.Length < 3)
                    {
                        throw new BadPolygonPointNumberException("Error in line 73: bad polygon point number");
                    }

                   // if (polygonPoint.Count != points.Length)
                   // {
                   //     throw new BadPolygonPointException("Error in line 87: bad polygon point");
                  //  }
                    else
                    {
                        _polygon = default;
                        _polygon = new PolygonFigure(points);
                        _isCommonReady = true;
                    }
                }
            }
            else
            {
                throw new BadFormatException("Error in line 78: bad format or unexpected end of polygon");
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

        public bool IsIntersected(ScenePoint p1, ScenePoint p2, ScenePoint p3, ScenePoint p4)
        {
            if (p2.X < p1.X)
            {
                ScenePoint tmp = p1;

                p1 = p2;

                p2 = tmp;
            }

            if (p4.X < p3.X)
            {
                ScenePoint tmp = p3;

                p3 = p4;

                p4 = tmp;
            }

            if (p2.X < p3.X)
            {
                return false;
            }

            if ((p1.X - p2.X == 0) && (p3.X - p4.X == 0))
            {
                if (p1.X == p3.X)
                {
                    if (!((Math.Max(p1.Y, p2.Y) < Math.Min(p3.Y, p4.Y)) ||

                          (Math.Min(p1.Y, p2.Y) > Math.Max(p3.Y, p4.Y))))
                    {
                        return true;
                    }
                }

                return false;
            }

// найдём коэффициенты уравнений, содержащих отрезк

// f1(x) = A1*x + b1 = y

// f2(x) = A2*x + b2 = y

// если первый отрезок вертикальный
            if (p1.X - p2.X == 0)
            {
                double xa, a2, b2, ya;
                xa = p1.X;

                a2 = (p3.Y - p4.Y) / (p3.X - p4.X);

                b2 = p3.Y - (a2 * p3.X);

                ya = (a2 * xa) + b2;

                if (p3.X <= xa && p4.X >= xa && Math.Min(p1.Y, p2.Y) <= ya &&

                    Math.Max(p1.Y, p2.Y) >= ya)
                {
                    return true;
                }

                return false;
            }

// если второй отрезок вертикальный
            if (p3.X - p4.X == 0)
            {
                double xa, a1, b1, ya;

// найдём Xa, Ya - точки пересечения двух прямых
                xa = p3.X;

                a1 = (p1.Y - p2.Y) / (p1.X - p2.X);

                b1 = p1.Y - (a1 * p1.X);

                ya = (a1 * xa) + b1;

                if (p1.X <= xa && p2.X >= xa && Math.Min(p3.Y, p4.Y) <= ya &&

                    Math.Max(p3.Y, p4.Y) >= ya)
                {
                    return true;
                }

                return false;
            }

// оба отрезка невертикальные
            {
                double a1 = (p1.Y - p2.Y) / (p1.X - p2.X);

                double a2 = (p3.Y - p4.Y) / (p3.X - p4.X);

                double b1 = p1.Y - (a1 * p1.X);

                double b2 = p3.Y - (a2 * p3.X);

                if (a1 == a2)
                {
                    return false;
                }

                double xa = (b2 - b1) / (a1 - a2);

                if ((xa < Math.Max(p1.X, p3.X)) || (xa > Math.Min(p2.X, p4.X)))
                {
                    return false; // точка Xa находится вне пересечения проекций отрезков на ось X
                }
                else
                {
                    return true;
                }
            }
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);
    }
}