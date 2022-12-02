namespace Scene2d.Figures
{
    using System;
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
            /* Should return new Rectangle with the same points as the current one. */

            throw new NotImplementedException();
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps current figure and has edges parallel to X and Y */

            throw new NotImplementedException();
        }

        public void Move(ScenePoint vector)
        {
            /* Should move all the points of current rectangle */

            throw new NotImplementedException();
        }

        public void Rotate(double angle)
        {
            /* Should rotate current rectangle. Rotation origin point is the rectangle center.*/

            throw new NotImplementedException();
        }

        public void Reflect(ReflectOrientation orientation)
        {
            /* Should reflect the figure. Reflection edge is the rectangle axis of symmetry (horizontal or vertical). */

            throw new NotImplementedException();
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
