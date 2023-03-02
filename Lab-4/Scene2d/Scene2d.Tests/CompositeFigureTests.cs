namespace Scene2d.Tests;

using NUnit.Framework;
using Scene2d.CommandBuilders;
using Scene2d.Exceptions;
using Scene2d.Figures;

[TestFixture]
public class CompositeFigureTests
{

    public CompositeFigure CreateCompositeFigure(double vectorX, double vectorY, int coordCount)
    {
        List<IFigure> composite = new List<IFigure>();
        var circle1 = new CircleFigure(new ScenePoint { X = vectorX, Y = vectorY }, 10);
        var circle2 = new CircleFigure(new ScenePoint { X = vectorX * 5, Y = vectorY * 5 }, 15);
        var circle3 = new CircleFigure(new ScenePoint { X = vectorX + 3.0, Y = vectorY + 5.2}, 30);
        var rect1 = new RectangleFigure(new ScenePoint { X = vectorX * 20, Y = vectorY / 10.5 }, new ScenePoint { X = vectorX * 13, Y = vectorY * 7 });
        var rect2 = new RectangleFigure(new ScenePoint { X = vectorX * 100, Y = vectorY * 2 }, new ScenePoint { X = vectorX * 105, Y = vectorY * 3 });
        var rect3 = new RectangleFigure(new ScenePoint { X = vectorX * 0.5, Y = vectorY }, new ScenePoint { X = vectorX * 2, Y = vectorY * 0.5});
        ScenePoint[] points = new ScenePoint[coordCount];
        for (var i = 0; i < coordCount; i++)
        {
            points[i] = new ScenePoint { X = i, Y = i * 5 };
        }

        var polygon = new PolygonFigure(points);

        composite.Add(circle1);
        composite.Add(circle2);
        composite.Add(circle3);
        composite.Add(rect1);
        composite.Add(rect2);
        composite.Add(rect3);
        composite.Add(polygon);

        return new CompositeFigure(composite);
    }

    [TestCase(5.3, 6.2, 5, TestName = "test1")]
    [TestCase(52.3, 16.2, 12, TestName = "test2")]
    [TestCase(15.3, 6.2, 50, TestName = "test3")]
    [TestCase(53.3, 16.2, 7, TestName = "test4")]
    [TestCase(13.3, 15.2, 20, TestName = "test5")]
    public void MoveTests_MoveVector_IsMoved(double vectorX, double vectorY, int coordCount)
    {
        // ARRANGE
        var TOLERANCE = 0.00001;
        var compositeFigure = CreateCompositeFigure(vectorX, vectorY, coordCount);

        // ACT
        var compositeFigureBasedRect = compositeFigure.CalculateCircumscribingRectangle();
        compositeFigure.Move(new ScenePoint {X = vectorX, Y = vectorY });
        var compositeFigureMovedRect = compositeFigure.CalculateCircumscribingRectangle();

        var isMoved = (Math.Abs(compositeFigureMovedRect.Vertex1.X - vectorX - compositeFigureBasedRect.Vertex1.X) < TOLERANCE)
                      && (Math.Abs(compositeFigureMovedRect.Vertex1.Y - vectorY - compositeFigureBasedRect.Vertex1.Y) < TOLERANCE)
                      && (Math.Abs(compositeFigureMovedRect.Vertex2.X - vectorX - compositeFigureBasedRect.Vertex2.X) < TOLERANCE)
                      && (Math.Abs(compositeFigureMovedRect.Vertex2.Y - vectorY - compositeFigureBasedRect.Vertex2.Y) < TOLERANCE);

        // ASSERT
        Assert.True(isMoved);
    }

    [TestCase("group heh, 1 to badgroup", TestName = "test1")]
    [TestCase("group heh, 1 as", TestName = "test2")]
    [TestCase("group heh, 1", TestName = "test3")]
    [TestCase("group badgroup", TestName = "test4")]
    [TestCase("g", TestName = "test5")]
    public void AddComposite_Command_ThrowException(string input)
    {
        // ARRANGE
        var compositeBuilder = new GroupCommandBuilder();

        // ACT
        // ASSERT
        Assert.Throws<BadFormatException>(() => compositeBuilder.AppendLine(input));
    }

    [TestCase(5.3, 6.2, 5, TestName = "test1")]
    [TestCase(52.3, 16.2, 12, TestName = "test2")]
    [TestCase(15.3, 6.2, 10, TestName = "test3")]
    [TestCase(53.3, 16.2, 7, TestName = "test4")]
    [TestCase(13.3, 15.2, 20, TestName = "test5")]
    public void CompositeCopy_FiguresCoord_IsCopy(double vectorX, double vectorY, int coordCount)
    {
        // ARRANGLE 
        var compositeFigure = CreateCompositeFigure(vectorX, vectorY, coordCount);
        var cloneCompositeFigure = (CompositeFigure)compositeFigure.Clone();

        // ACT
        var circumscribingBased = compositeFigure.CalculateCircumscribingRectangle();
        var circumscribingClone = cloneCompositeFigure.CalculateCircumscribingRectangle();
        bool isClone = Equals(compositeFigure, cloneCompositeFigure);

        // ASSERT
        Assert.True(isClone);
    }
}