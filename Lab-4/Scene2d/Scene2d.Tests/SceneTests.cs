namespace Scene2d.Tests;

using System.Diagnostics;
using NUnit.Framework;
using Scene2d.Figures;

public class SceneTests
{
    public Scene CreateScene(double vectorX, double vectorY, int coordCount)
    {
        Scene scene = new Scene();
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
        scene.AddFigure("one", circle1);
        scene.AddFigure("two", circle2);
        scene.AddFigure("3two", circle3);
        scene.AddFigure("4two", rect1);
        scene.AddFigure("5two", rect2);
        scene.AddFigure("6two", rect3);
        scene.AddFigure("7two", polygon);

        return scene;
    }

    [TestCase(5.3, 6.2, 5)]
    [TestCase(52.3, 16.2, 12)]
    [TestCase(15.3, 6.2, 50)]
    [TestCase(53.3, 16.2, 7)]
    [TestCase(13.3, 15.2, 20)]
    public void MoveSceneTest_Coords_Vector_IsMoved(double vectorX, double vectorY, int coordCount)
    {
        // ARRANGE
        var scene = CreateScene(vectorX, vectorY, coordCount);
        double TOLERANCE = 0.00001;

        // ACT
        var basedCircumscribingRect = scene.CalculateSceneCircumscribingRectangle();
        scene.MoveScene(new ScenePoint { X = vectorX, Y = vectorY });
        var exceptedCircumscribingRect = scene.CalculateSceneCircumscribingRectangle();
        var isMoved = (Math.Abs(exceptedCircumscribingRect.Vertex1.X - vectorX - basedCircumscribingRect.Vertex1.X) < TOLERANCE)
                      && (Math.Abs(exceptedCircumscribingRect.Vertex1.Y - vectorY - basedCircumscribingRect.Vertex1.Y) < TOLERANCE)
                      && (Math.Abs(exceptedCircumscribingRect.Vertex2.X - vectorX - basedCircumscribingRect.Vertex2.X) < TOLERANCE)
                      && (Math.Abs(exceptedCircumscribingRect.Vertex2.Y - vectorY - basedCircumscribingRect.Vertex2.Y) < TOLERANCE);

        // ASSERT
        Assert.True(isMoved);
    }

}