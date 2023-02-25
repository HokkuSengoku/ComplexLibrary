namespace Scene2d.Commands;

public class CopyCommand : ICommand
{
    private readonly string _name;
    private readonly string _nameTo;
    private readonly bool _isScene;

    public CopyCommand(string name, string nameTo, bool isScene)
    {
        _name = name;
        _nameTo = nameTo;
        _isScene = isScene;
    }

    public CopyCommand(string nameTo, bool isScene)
    {
        _nameTo = nameTo;
        _isScene = isScene;
    }

    public string FriendlyResultMessage
    {
        get
        {
            if (_isScene)
            {
                return "Copy Scene";
            }
            else
            {
                return $"Copy figure or group {_name} to new figure or group with name  {_nameTo}";
            }
        }
    }

    public void Apply(Scene scene)
    {
        if (_isScene)
        {
            scene.CopyScene(_nameTo);
        }
        else
        {
            scene.Copy(_name, _nameTo);
        }
    }
}