namespace Scene2d.Tests;

using System.Diagnostics;
using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Exceptions;
using Scene2d.Figures;

public class RectangleTests
{
    [TestCase(0.0, 0.0, 2.0, 2.0, 3.0, 3.0)]
    public void MoveTest_CoordinatesAndVector_IsMoved(double p1X, double p1y, double p2x, double p2y, double vectorx, double vectory)
    {
        //ARRANGE
        var rectangle = new RectangleFigure(new ScenePoint { X = p1X, Y = p1y }, new ScenePoint { X = p2x, Y = p2y });
        ScenePoint vectorMove = new ScenePoint { X = vectorx, Y = vectory };
        
        //ACT
        rectangle.Move(vectorMove);
        var rectangleMovedExcepted =
            new RectangleFigure(new ScenePoint { X = p1X + vectorx, Y = p1y + vectory }, new ScenePoint { X = p2x + vectorx, Y = p2y + vectory }).CalculateCircumscribingRectangle();
        var actualMovedRectangle = rectangle.CalculateCircumscribingRectangle();
        
        //ASSERT
        Assert.AreEqual(rectangleMovedExcepted, actualMovedRectangle);
    }

    [TestCase("add rectangle t1 (0, 0) (k, k)")]
    public void AddRectangleTest_stringInput_IsBadFormat(string input)
    {
        var rectangle = new AddRectangleCommandBuilder();

        Assert.Throws<BadFormatException>(() => rectangle.AppendLine(input));
    }

    public void AddRectangleTest_stringInput_IsBadRectanglePointException(string input)
    {
        
    }
}
