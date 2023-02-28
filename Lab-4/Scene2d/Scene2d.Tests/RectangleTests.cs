namespace Scene2d.Tests;

using System.Diagnostics;
using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Commands;
using Scene2d.Exceptions;
using Scene2d.Figures;

public class RectangleTests
{
    public RectangleFigure CreateRectangle(string command)
    {
        var separators = new char[] { ' ', '(', ')', ',' };
        var coordinates = new double[4];
        var stringCommand = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var rectangle = new AddRectangleCommandBuilder();
        coordinates = rectangle.GetCoordinates(stringCommand);

        return new RectangleFigure(
            new ScenePoint { X = coordinates[0], Y = coordinates[1] },
            new ScenePoint { X = coordinates[2], Y = coordinates[3] });
    }

    [TestCase(5.3, 7.8, 20.4, 70.2, 30.5, 10.6)]
    [TestCase(0.0, 0.0, 2.0, 2.0, 3.0, 3.0)]
    public void MoveTest_CoordinatesAndVector_IsMoved(double p1X, double p1Y, double p2X, double p2Y, double vectorX, double vectorY)
    {
        // ARRANGE
        var rectangle = new RectangleFigure(new ScenePoint { X = p1X, Y = p1Y }, new ScenePoint { X = p2X, Y = p2Y });
        ScenePoint vectorMove = new ScenePoint { X = vectorX, Y = vectorY };

        // ACT
        rectangle.Move(vectorMove);
        var rectangleMovedExcepted =
            new RectangleFigure(new ScenePoint { X = p1X + vectorX, Y = p1Y + vectorY }, new ScenePoint { X = p2X + vectorX, Y = p2Y + vectorY });
        var actualMovedRectangle = rectangle;

        // ASSERT
        Assert.True(Equals(actualMovedRectangle, rectangleMovedExcepted));
    }

    [TestCase("add rectangle t1 (0, 0) (k, k)")]
    [TestCase("add rectangle x")]
    public void AddRectangleTest_stringInput_IsBadFormat(string input)
    {
        // ARRANGE
        var rectangle = new AddRectangleCommandBuilder();

        // ACT

        // ASSERT
        Assert.Throws<BadFormatException>(() => rectangle.AppendLine(input));
    }

    [TestCase("add rectangle t1 (0, 0) (0, 0)")]
    [TestCase("add rectangle t2 (0, 2) (2, 2)")]
    public void AddRectangleTest_stringInput_IsBadRectanglePointException(string input)
    {
        // ARRANGE
        var rectangle = new AddRectangleCommandBuilder();

        // ACT
        // ASSERT
        Assert.Throws<BadRectanglePointException>( () => rectangle.AppendLine(input));
    }

    [TestCase(5.6, 7.3, 20.5, 30.6)]
    [TestCase(500.3, 234.5, 560.2, 722.3)]
    public void CloneRectangleTest_PointCoord_IsClone(double p1X, double p1Y, double p2X, double p2Y)
    {
        // ARRANGE
        var origin = new RectangleFigure(new ScenePoint { X = p1X, Y = p1Y }, new ScenePoint { X = p2X, Y = p2Y });
        var copy = (RectangleFigure)origin.Clone();

        // ACT
        bool isCopy = Equals(origin, copy);

        // ASSERT
        Assert.True(isCopy);
    }

    [TestCase(0.0, TestName = "test1")]
    public void RotateTest_CoordsAndAngle_IsRotated(double angle)
    {
        // This test could not be completed due to the inability to process the error
        // ARRANGE
        var origin = new RectangleFigure(new ScenePoint { X = 2.0, Y = 2.0 }, new ScenePoint { X = 5.0, Y = 5.0 });
        var basedRect = new RectangleFigure(new ScenePoint { X = 2.0, Y = 2.0 }, new ScenePoint { X = 5.0, Y = 5.0 });

        origin.Rotate(angle);
        var expectedRect = origin;

        Assert.True(Equals(expectedRect, basedRect));
    }

    [TestCase("add rectangle t1 (0, 5) (20, 7)", TestName = "test1")]
    [TestCase("add rectangle recto (2, 5) (20, 7)", TestName = "test2")]
    [TestCase("add rectangle codemasters (10, 15) (200, 37)", TestName = "test3")]
    [TestCase("add rectangle ___ (12, 52) (220, 72)", TestName = "test4")]
    [TestCase("add rectangle f1_ (220, 53) (430, 87)", TestName = "test5")]
    public void AddRectBuilder_command_IsAdded(string command)
    {
        // ARRANGE
        string name = command.Split()[2];
        var rectangleOrigin = new AddRectangleCommandBuilder();
        rectangleOrigin.AppendLine(command);
        var rectangleExpected = CreateRectangle(command);
        
        // ACT
        bool isAdded = Equals(rectangleOrigin.GetCommand(), new AddFigureCommand(name, rectangleExpected));
        // ASSERT
        Assert.True(isAdded);
    }
}
