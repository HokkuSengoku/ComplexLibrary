namespace Scene2d.Figures
{
    using System.Drawing;

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
            throw new System.NotImplementedException();
        }

        public void Rotate(double angle)
        {
            throw new System.NotImplementedException();
        }

        public void Reflect(ReflectOrientation orientation)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}