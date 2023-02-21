namespace Scene2d.Figures;

using System;
using System.Collections.Generic;
using System.Drawing;

public class CompositeFigure : ICompositeFigure
{
    private string _name;

    public CompositeFigure(List<IFigure> figures, string name)
    {
        _name = name;
        foreach (var figure in figures)
        {
            ChildFigures.Add(figure);
        }
    }

    public IList<IFigure> ChildFigures { get; }

    public object Clone()
    {
        throw new System.NotImplementedException();
    }

    public SceneRectangle CalculateCircumscribingRectangle()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public void Draw(ScenePoint origin, Graphics drawing)
    {
        foreach (var variablChildFigure in ChildFigures)
        {
            variablChildFigure.Draw(origin, drawing);
        }
    }
}