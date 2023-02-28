namespace Scene2d.Figures
{
    using System;
    using System.Drawing;
    using System.Numerics;

    public class CircleFigure : IFigure
    {
        private ScenePoint _center;
        private double _radius;

        public CircleFigure(ScenePoint center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            var x1 = _center.X - _radius;
            var y1 = _center.Y - _radius;
            var x2 = _center.X + _radius;
            var y2 = _center.Y + _radius;

            SceneRectangle aFlowingRectangle = default;
            aFlowingRectangle.Vertex1 = new ScenePoint(x1, y1);
            aFlowingRectangle.Vertex2 = new ScenePoint(x2, y2);

            return aFlowingRectangle;
        }

        public void Move(ScenePoint vector)
        {
            _center = new ScenePoint { X = _center.X + vector.X, Y = _center.Y + vector.Y };
        }

        public void Rotate(double angle)
        {
        }

        public void Reflect(ReflectOrientation orientation)
        {
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */
            using (var pen = new Pen(Color.Green))
            {
                drawing.DrawEllipse(
                    pen,
                    (int)(_center.X - _radius - origin.X),
                    (int)(_center.Y - _radius - origin.Y),
                    (int)(_radius * 2),
                    (int)(_radius * 2));
            }
        }

        public object Clone()
        {
            double radius = _radius;
            ScenePoint center = _center;
            return new CircleFigure(center, radius);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CircleFigure);
        }

        public bool Equals(CircleFigure circle)
        {
            return Equals(_center, circle._center) && _radius == _radius;
        }

        public override int GetHashCode()
        {
            var hash = default(HashCode);
            hash.Add(_center);
            hash.Add(_radius);

            return hash.ToHashCode();
        }
    }
}