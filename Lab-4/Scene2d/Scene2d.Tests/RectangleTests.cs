namespace Scene2d.Tests;

using NUnit.Framework;
using Scene2d.Figures;

public class RectangleTests
{
    [TestCase(0.0, 0.0, 2.0, 2.0, 3.0)]
    public void MoveTest(double p1X, double p1y, double p2x, double p2y, double vector)
    {
        var basedRectangle = new RectangleFigure(new ScenePoint { X = p1X, Y = p1y }, new ScenePoint { X = p2x, Y = p2y });
        var rectangle = new RectangleFigure(new ScenePoint { X = p1X, Y = p1y }, new ScenePoint { X = p2x, Y = p2y });
        ScenePoint vectorMove = new ScenePoint { X = vector, Y = vector };
        rectangle.Move(vectorMove);

        Assert.AreEqual(basedRectangle, rectangle);
    }
}
