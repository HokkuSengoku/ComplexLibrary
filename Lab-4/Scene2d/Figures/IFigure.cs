namespace Scene2d.Figures
{
    using System;
    using System.Drawing;

    public interface IFigure : ICloneable
    {
        SceneRectangle CalculateCircumscribingRectangle();

        void Move(ScenePoint vector);

        void Rotate(double angle);

        void Reflect(ReflectOrientation orientation);

        void Draw(ScenePoint origin, Graphics drawing);
    }
}
