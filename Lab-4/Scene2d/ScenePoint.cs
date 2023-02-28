namespace Scene2d
{
    using System;
    using System.Collections;
    using System.Drawing;

    public struct ScenePoint
    {
        public ScenePoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            return Equals((ScenePoint)obj);
        }

        public bool Equals(ScenePoint scenePoint)
        {
            return X == scenePoint.X && Y == scenePoint.Y;
        }

        private static void RotatePoint()
        {
        }
    }
}
