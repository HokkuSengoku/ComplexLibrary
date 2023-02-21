namespace Scene2d.Commands;

using System.Collections.Generic;

public class GroupCommand : ICommand
{
    private List<string> _compositeFigure = new List<string>();
    private string _name;

    public GroupCommand(string name, List<string> compositeFigure)
    {
        _name = name;
        _compositeFigure = compositeFigure;
    }

    public string FriendlyResultMessage { get; }

    public void Apply(Scene scene)
    {
        scene.CreateCompositeFigure(_name, _compositeFigure);
    }
}