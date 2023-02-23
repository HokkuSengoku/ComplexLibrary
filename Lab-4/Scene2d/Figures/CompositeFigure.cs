namespace Scene2d.Figures;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class CompositeFigure : ICompositeFigure
{
    public CompositeFigure(IList<IFigure> figures)
    {
        ChildFigures = figures;
    }

    public IList<IFigure> ChildFigures { get; }

    public object Clone()
    {
        IList<IFigure> figures = new List<IFigure>();
        foreach (var figure in ChildFigures)
        {
            figures.Add((IFigure)figure.Clone());
        }

        return new CompositeFigure(figures);
    }

    public SceneRectangle CalculateCircumscribingRectangle()
    {
        List<ScenePoint> minVertex = new List<ScenePoint>();
        List<ScenePoint> maxVertex = new List<ScenePoint>();
        ScenePoint resultMinVertex;
        ScenePoint resultMaxVertex;

        foreach (var figure in ChildFigures)
        {
            var currentRectangle = figure.CalculateCircumscribingRectangle();
            minVertex.Add(currentRectangle.Vertex1);
            maxVertex.Add(currentRectangle.Vertex2);
        }

        resultMinVertex = minVertex.Min();
        resultMaxVertex = maxVertex.Max();
        SceneRectangle circumscribedRectangle = default;
        circumscribedRectangle.Vertex1 = resultMinVertex;
        circumscribedRectangle.Vertex2 = resultMaxVertex;

        return circumscribedRectangle;
    }

    public void Move(ScenePoint vector)
    {
        foreach (var figure in ChildFigures)
        {
            figure.Move(vector);
        }
    }

    public void Rotate(double angle)
    {
        foreach (var figure in ChildFigures)
        {
            figure.Rotate(angle);
        }
    }

    public void Reflect(ReflectOrientation orientation)
    {
        foreach (var figure in ChildFigures)
        {
            figure.Reflect(orientation);
        }
    }

    public void Draw(ScenePoint origin, Graphics drawing)
    {
        foreach (var variablChildFigure in ChildFigures)
        {
            variablChildFigure.Draw(origin, drawing);
        }
    }
}