namespace Scene2d.Commands;

public class DeleteCommand : ICommand
{
    private readonly string _name;
    private readonly bool _isScene;

    public DeleteCommand(string name, bool isScene)
    {
        _name = name;
        _isScene = isScene;
    }

    public DeleteCommand(bool isScene)
    {
        _isScene = isScene;
    }

    public string FriendlyResultMessage
    {
        get
        {
            if (_isScene)
            {
                return "Delete Scene";
            }
            else
            {
                return $"Delete figure or group {_name}";
            }
        }
    }

    public void Apply(Scene scene)
    {
        if (_isScene)
        {
            scene.DeleteScene();
        }
        else
        {
            scene.Delete(_name);
        }
    }
}