namespace Scene2d.Figures
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class RectangleFigure : IFigure
    {
        /* We store four rectangle points because after rotation edges could be not parallel to XY axes. */
        private ScenePoint _p1;
        private ScenePoint _p2;
        private ScenePoint _p3;
        private ScenePoint _p4;

        public RectangleFigure(ScenePoint p1, ScenePoint p2)
        {
            _p1 = p1;
            _p2 = new ScenePoint { X = p2.X, Y = p1.Y };
            _p3 = p2;
            _p4 = new ScenePoint { X = p1.X, Y = p2.Y };
        }

        public object Clone()
        {
            ScenePoint p1 = _p1;
            ScenePoint p2 = _p3;

            return new RectangleFigure(p1, p2);
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps current figure and has edges parallel to X and Y */

              double xMin = Math.Min(Math.Min(_p1.X, _p2.X), Math.Min(_p3.X, _p4.X));
              double xMax = Math.Max(Math.Max(_p1.X, _p2.X), Math.Max(_p3.X, _p4.X));
              double yMin = Math.Min(Math.Min(_p1.Y, _p2.Y), Math.Min(_p3.Y, _p4.Y));
              double yMax = Math.Max(Math.Max(_p1.Y, _p2.Y), Math.Max(_p3.Y, _p4.Y));

              SceneRectangle circumscribedRectangle = default;
              circumscribedRectangle.Vertex1 = new ScenePoint(xMin, yMin);
              circumscribedRectangle.Vertex2 = new ScenePoint(xMax, yMax);

              return circumscribedRectangle;
        }

        public void Move(ScenePoint vector)
        {
            _p1 = new ScenePoint { X = _p1.X + vector.X, Y = _p1.Y + vector.Y };
            _p2 = new ScenePoint { X = _p2.X + vector.X, Y = _p2.Y + vector.Y };
            _p3 = new ScenePoint { X = _p3.X + vector.X, Y = _p3.Y + vector.Y };
            _p4 = new ScenePoint { X = _p4.X + vector.X, Y = _p4.Y + vector.Y };
        }

        public void Rotate(double angle)
        {
            ScenePoint point = CalculateCircumscribingRectangle().CalculateTheCenterOfRectangle();

            _p1 = _p1.RotatePoint(point, angle);
            _p2 = _p2.RotatePoint(point, angle);
            _p3 = _p3.RotatePoint(point, angle);
            _p4 = _p4.RotatePoint(point, angle);
        }

        public void Reflect(ReflectOrientation orientation)
        {
            ScenePoint centerPoint = CalculateCircumscribingRectangle().CalculateTheCenterOfRectangle();

            switch (orientation)
            {
                case ReflectOrientation.Horizontal:
                    {
                        _p1 = new ScenePoint { X = (-1 * (_p1.X - centerPoint.X)) + centerPoint.X, Y = _p1.Y };
                        _p2 = new ScenePoint { X = (-1 * (_p2.X - centerPoint.X)) + centerPoint.X, Y = _p2.Y };
                        _p3 = new ScenePoint { X = (-1 * (_p3.X - centerPoint.X)) + centerPoint.X, Y = _p3.Y };
                        _p4 = new ScenePoint { X = (-1 * (_p4.X - centerPoint.X)) + centerPoint.X, Y = _p4.Y };
                        break;
                    }

                case ReflectOrientation.Vertical:
                    {
                        _p1 = new ScenePoint { X = _p1.X, Y = (-1 * (_p1.Y - centerPoint.Y)) + centerPoint.Y };
                        _p2 = new ScenePoint { X = _p2.X, Y = (-1 * (_p2.Y - centerPoint.Y)) + centerPoint.Y };
                        _p3 = new ScenePoint { X = _p3.X, Y = (-1 * (_p3.Y - centerPoint.Y)) + centerPoint.Y };
                        _p4 = new ScenePoint { X = _p4.X, Y = (-1 * (_p4.Y - centerPoint.Y)) + centerPoint.Y };
                        break;
                    }
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */

            using (var pen = new Pen(Color.Blue))
            {
                drawing.DrawLine(
                    pen,
                    (float)(_p1.X - origin.X),
                    (float)(_p1.Y - origin.Y),
                    (float)(_p2.X - origin.X),
                    (float)(_p2.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p2.X - origin.X),
                    (float)(_p2.Y - origin.Y),
                    (float)(_p3.X - origin.X),
                    (float)(_p3.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p3.X - origin.X),
                    (float)(_p3.Y - origin.Y),
                    (float)(_p4.X - origin.X),
                    (float)(_p4.Y - origin.Y));

                drawing.DrawLine(
                    pen,
                    (float)(_p4.X - origin.X),
                    (float)(_p4.Y - origin.Y),
                    (float)(_p1.X - origin.X),
                    (float)(_p1.Y - origin.Y));
            }
        }
    }
}
