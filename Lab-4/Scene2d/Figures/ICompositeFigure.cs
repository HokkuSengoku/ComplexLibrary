namespace Scene2d.Figures
{
    using System.Collections.Generic;

    /* This interface is not implemented yet */
    public interface ICompositeFigure : IFigure
    {
        IList<IFigure> ChildFigures { get; }
    }
}
