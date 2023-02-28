namespace Scene2d.Tests;

using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Commands;
using Scene2d.Exceptions;
using Scene2d.Figures;

public class PolygonTests
{

    public PolygonFigure CreatePolygon(string[] command)
    {
        var polygon = new AddPolygonCommandBuilder();
        var separators = new char[] { ' ', '(', ')', ',' };
        List<string[]> coords = new List<string[]>();
        List<double[]> coordinates = new List<double[]>();
        for (var i = 1; i < command.Length - 1; i++)
        {
            coords.Add(command[i].Split(separators, StringSplitOptions.RemoveEmptyEntries));
        }

        foreach (var item in coords)
        {
            coordinates.Add(polygon.GetCoordinates(item));
        }

        ScenePoint[] points = new ScenePoint[coordinates.Count];
        for (var i = 0; i < coordinates.Count; i++)
        {
            points[i] = new ScenePoint { X = coordinates[i][0], Y = coordinates[i][1] };
        }

        return new PolygonFigure(points);
    }

    [TestCase(5, 3.2, 6.7)]
    [TestCase(7, 30.2, 60.7)]
    [TestCase(3, 32.2, 6.7)]
    [TestCase(4, 14.2, 2.7)]
    public void MoveTest_CoordinatesAndVector(int coordCount, double vectorX, double vectorY)
    {
        // ARRANGE
        ScenePoint[] points = new ScenePoint[coordCount];
        ScenePoint[] movedPoints = new ScenePoint[coordCount];
        for (var i = 0; i < coordCount; i++)
        {
            points[i] = new ScenePoint { X = i, Y = i * 5 };
            movedPoints[i] = new ScenePoint { X = points[i].X + vectorX, Y = points[i].Y + vectorY };
        }

        var polygon = new PolygonFigure(points);
        var movedPolygon = new PolygonFigure(movedPoints);

        // ACT
        polygon.Move(new ScenePoint{X = vectorX, Y = vectorY });
        var isMoved = Equals(polygon, movedPolygon);

        // ASSERT
        Assert.True(isMoved);
    }

    [TestCase("add1")]
    [TestCase("ENddgdgdfgd")]
    [TestCase("add1 polygon tes add point(3, 2)")]
    public void AddPolygon_StringCommand_ThrowException(string input)
    {
        // ARRANGE
        var polygon = new AddPolygonCommandBuilder();

        // ACT

        // ASSERT
        Assert.Throws<BadFormatException>((() => polygon.AppendLine(input)));
    }

    [TestCase(5)]
    [TestCase(7)]
    [TestCase(10)]
    [TestCase(15)]
    [TestCase(20)]
    public void PolygonClone_Coords_IsCloned(int coordCount)
    {
        // ARRANGE
        ScenePoint[] points = new ScenePoint[coordCount];
        for (var i = 0; i < coordCount; i++)
        {
            points[i] = new ScenePoint { X = i, Y = i * 5 };
        }

        var polygon = new PolygonFigure(points);

        // ACT
        var clonePolygon = (PolygonFigure)polygon.Clone();
        
        var isCloned = Equals(polygon, clonePolygon);

        // ASSERT
        Assert.True(isCloned);
    }

    [TestCase(new object[] { "add polygon test1", " add point (2, 2)", " add point (3, 4)", " add point (6, 7)", "end polygon" }, TestName = "test1")]
    [TestCase(new object[] { "add polygon test1", " add point (2, 2)", " add point (3, 4)", " add point (6, 7)", "end polygon" }, TestName = "test2")]
    [TestCase(new object[] { "add polygon test1", " add point (2, 2)", " add point (3, 4)", " add point (6, 7)", " add point (26, 37)", "end polygon" }, TestName = "test3")]
    [TestCase(new object[] { "add polygon test1", " add point (2, 2)", " add point (3, 4)", " add point (6, 7)", " add point (15, 17)", " add point (266, 37)", "end polygon" }, TestName = "test4")]
    public void AddPolygonTest_commands_IsAdded(params string[] commands)
    {
        // ARRANGE
        var separators = new char[] { ' ', '(', ')', ',' };
        var name = commands[0].Split(separators, StringSplitOptions.RemoveEmptyEntries)[2];

        // ACT
        var polygonExpected = CreatePolygon(commands);
        var polygon = new AddPolygonCommandBuilder();
        foreach (var command in commands)
        {
            polygon.AppendLine(command);
        }

        bool isTrue = Equals(polygon.GetCommand(), new AddFigureCommand(name, polygonExpected));

        // ASSERT
        Assert.True(isTrue);
    }
}