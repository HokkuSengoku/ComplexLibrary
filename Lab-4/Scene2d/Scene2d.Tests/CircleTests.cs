namespace Scene2d.Tests;

using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Commands;
using Scene2d.Exceptions;
using Scene2d.Figures;

public class CircleTests
{
    public CircleFigure CreateCircleFigure(string command)
    {
        var separators = new char[] { ' ', '(', ')', ',' };
        var coordinates = new double[2];
        var circle = new AddCircleCommandBuilder();
        double radius;
        var commandResult = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        coordinates = circle.GetCoordinates(commandResult);
        radius = Convert.ToDouble(commandResult[6]);

        return new CircleFigure(new ScenePoint(coordinates[0], coordinates[1]), radius);
    }
    
    [TestCase(2.0, 3.5, 20.3, 16.5, TestName = "test1")]
    [TestCase(5.0, 3.5, 201.3, 6.5, TestName = "test2")]
    [TestCase(21.0, 13.5, 120.3, 36.5, TestName = "test3")]
    [TestCase(22.0, 333.5, 202.3, 63.5, TestName = "test4")]
    [TestCase(221.0, 32.5, 210.3, 60.5, TestName = "test5")]
    public void MoveTest_Coordinate_IsMoved(double centerX, double centerY, double moveX, double moveY)
    {
        // ARRANGE
        double radius = 5;
        var circle = new CircleFigure(new ScenePoint {X = centerX, Y = centerY}, radius);
        ScenePoint vector = new ScenePoint { X = moveX, Y = moveY };
        var movedCircleExpected = new CircleFigure(new ScenePoint { X = centerX + moveX, Y = centerY + moveY }, radius);

        // ACT
        circle.Move(vector);
        var movedCircle = circle;
        // ASSERT
        Assert.True(Equals(circle, movedCircleExpected));
    }

    [TestCase("add circle c1 (0, 0) ", TestName = "test1")]
    [TestCase("add circle c1 (0, 0) radius a", TestName = "test2")]
    [TestCase("add circle c1 (0, 0) (3,2)", TestName = "test3")]
    [TestCase("add circle  (0, 0) ", TestName = "test4")]
    public void AddCircleTest_Command_BadFormatException(string input)
    {
        // ARRANGE
        var circle = new AddCircleCommandBuilder();

        // ACT

        // ASSERT
        Assert.Throws<BadFormatException>(() => circle.AppendLine(input));
    }

    [TestCase("add circle c1 (0, 5) radius 0", TestName = "test1")]
    [TestCase("add circle c1 (0, 5) radius -2", TestName = "test2")]
    [TestCase("add circle c1 (0, 5) radius -100", TestName = "test3")]
    [TestCase("add circle c1 (0, 5) radius -0.11", TestName = "test4")]
    public void AddCircleTest_Command_BadCircleRadiusException(string input)
    {
        // ARRANGE
        var circle = new AddCircleCommandBuilder();

        // ACT

        // ASSERT
        Assert.Throws<BadCircleRadiusException>((() => circle.AppendLine(input)));
    }

    [TestCase(5.3, 22.3, 7.0, TestName = "test1")]
    [TestCase(50.3, 12.3, 71.0, TestName = "test2")]
    [TestCase(25.3, 2.363, 72.0, TestName = "test3")]
    [TestCase(65.32, 12.322, 2.0, TestName = "test4")]
    [TestCase(135.35, 52.323, 15.0, TestName = "test5")]
    public void CloneCircleTest_CoordsAndRadius_IsCloned(double p1X, double p1Y, double radius)
    {
        // ARRANGE
        var circleOrigin = new CircleFigure(new ScenePoint { X = p1X, Y = p1Y }, radius);
        var circleClone = (CircleFigure)circleOrigin.Clone();

        // ACT

        // ASSERT
        Assert.True(Equals(circleOrigin, circleClone));

    }

    [TestCase("add circle c1 (0, 0) radius 50", TestName = "test1")]
    [TestCase("add circle c2 (32, 10) radius 20", TestName = "test2")]
    [TestCase("add circle c1 (50, 20) radius 5", TestName = "test3")]
    [TestCase("add circle c1 (100, 1000) radius 200", TestName = "test4")]
    [TestCase("add circle c1 (3, 2) radius 1", TestName = "test5")]
    public void AddCircleBuilder_Command_IsAdded(string command)
    {
        // ARRANGE
        var circleCreate = CreateCircleFigure(command);
        var circleCommand = new AddCircleCommandBuilder();
        var name = command.Split()[2];
        circleCommand.AppendLine(command);

        // ACT
        // ASSERT
        Assert.True(Equals(circleCommand.GetCommand(), new AddFigureCommand(name, circleCreate)));
    }
}