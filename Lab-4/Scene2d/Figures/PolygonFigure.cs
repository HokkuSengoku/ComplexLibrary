namespace Scene2d.Figures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class PolygonFigure : IFigure
    {
        private ScenePoint[] _points;

        public PolygonFigure(ScenePoint[] points)
        {
            _points = points;
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            double x1, y1, x2, y2;
            var xValues = new List<double>();
            var yValues = new List<double>();

            foreach (var point in _points)
            {
                xValues.Add(point.X);
                yValues.Add(point.Y);
            }

            x1 = xValues.Min();
            y1 = yValues.Min();
            x2 = xValues.Max();
            y2 = yValues.Max();

            SceneRectangle aFlowingRectangle = default;
            aFlowingRectangle.Vertex1 = new ScenePoint(x1, y1);
            aFlowingRectangle.Vertex2 = new ScenePoint(x2, y2);

            return aFlowingRectangle;
        }

        public void Move(ScenePoint vector)
        {
            for (var i = 0; i < _points.Length; i++)
            {
                _points[i] = new ScenePoint { X = _points[i].X + vector.X, Y = _points[i].Y + vector.Y };
            }
        }

        public void Rotate(double angle)
        {
            ScenePoint point = CalculateCircumscribingRectangle().CalculateTheCenterOfRectangle();

            for (var i = 0; i < _points.Length; i++)
            {
                _points[i] = _points[i].RotatePoint(point, angle);
            }
        }

        public void Reflect(ReflectOrientation orientation)
        {
            ScenePoint centerPoint = CalculateCircumscribingRectangle().CalculateTheCenterOfRectangle();

            switch (orientation)
            {
                case ReflectOrientation.Horizontal:
                {
                    for (var i = 0; i < _points.Length; i++)
                    {
                        _points[i] = new ScenePoint { X = (-1 * (_points[i].X - centerPoint.X)) + centerPoint.X, Y = _points[i].Y };
                    }

                    break;
                }

                case ReflectOrientation.Vertical:
                {
                    for (var i = 0; i < _points.Length; i++)
                    {
                        _points[i] = new ScenePoint { X = _points[i].X, Y = (-1 * (_points[i].Y - centerPoint.Y)) + centerPoint.Y };
                    }

                    break;
                }
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */

            using (var pen = new Pen(Color.DarkOrchid))
            {
                for (var i = 0; i < _points.Length; i++)
                {
                    ScenePoint firstPoint = _points[i];
                    ScenePoint secondPoint = i >= _points.Length - 1 ? _points.First() : _points[i + 1];

                    drawing.DrawLine(
                        pen,
                        (float)(firstPoint.X - origin.X),
                        (float)(firstPoint.Y - origin.Y),
                        (float)(secondPoint.X - origin.X),
                        (float)(secondPoint.Y - origin.Y));
                }
            }
        }

        public object Clone()
        {
            ScenePoint[] points;
            List<ScenePoint> convertPoints = new List<ScenePoint>();
            foreach (var point in _points)
            {
                convertPoints.Add(point);
            }

            points = convertPoints.ToArray();
            return new PolygonFigure(points);
        }
    }
}