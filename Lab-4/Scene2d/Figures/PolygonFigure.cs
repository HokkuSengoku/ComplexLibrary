namespace Scene2d.Figures
{
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
            x1 = _points[0].X;
            x2 = _points[0].X;
            y1 = _points[0].Y;
            y2 = _points[0].Y;

            for (var i = 1; i < _points.Length; i++)
            {
                if (_points[i].X < x1)
                {
                    x1 = _points[i].X;
                }

                if (_points[i].Y < y1)
                {
                    y1 = _points[i].Y;
                }

                if (_points[i].X > x2)
                {
                    x2 = _points[i].X;
                }

                if (_points[i].Y > y2)
                {
                    y2 = _points[i].Y;
                }
            }

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
            throw new System.NotImplementedException();
        }
    }
}