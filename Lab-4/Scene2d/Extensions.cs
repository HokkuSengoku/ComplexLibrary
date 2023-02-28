namespace Scene2d
{
    using System;

    public static class Extensions
    {
        public static ScenePoint RotatePoint(this ScenePoint currentPoint, ScenePoint rotatePoint, double angle)
        {
            return new ScenePoint
            {
                X = ((currentPoint.X - rotatePoint.X) * Math.Cos(angle)) -
                    ((currentPoint.Y - rotatePoint.Y) * Math.Sin(angle)) + rotatePoint.X,
                Y = ((currentPoint.X - rotatePoint.X) * Math.Sin(angle)) +
                    ((currentPoint.Y - rotatePoint.Y) * Math.Cos(angle)) + rotatePoint.Y,
            };
        }
    }
}