namespace Scene2d.Commands;

public class PrintCircumscribingRectangleCommand : ICommand
{
    private string _name;
    private bool _isScene;

    public PrintCircumscribingRectangleCommand(string name, bool isScene)
    {
        _name = name;
        _isScene = isScene;
    }

    public PrintCircumscribingRectangleCommand(bool isScene)
    {
        _isScene = isScene;
    }

    public string FriendlyResultMessage { get; }

    public void Apply(Scene scene)
    {
        if (_isScene)
        {
            scene.PrintCircumscribingRectangleScene();
        }
        else
        {
            scene.PrintCircumscribingRectangle(_name);
        }
    }
}