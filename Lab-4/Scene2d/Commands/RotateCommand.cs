namespace Scene2d.Commands;

using Scene2d.CommandBuilders;

public class RotateCommand : ICommand
{
    private readonly string _name;

    private readonly double _angle;

    private readonly bool _shapeOrScene;

    public RotateCommand(string name, double angle, bool shapeOrScene)
    {
        _name = name;
        _angle = angle;
        _shapeOrScene = shapeOrScene;
    }

    public RotateCommand(double angle, bool shapeOrScene)
    {
        _angle = angle;
        _shapeOrScene = shapeOrScene;
    }

    public string FriendlyResultMessage
    {
        get
        {
            if (_shapeOrScene != true)
            {
                return "Rotate Scene";
            }
            else
            {
                return $"Rotate figure {_name} by angle = {_angle}";
            }
        }
    }

    public void Apply(Scene scene)
    {
        if (_shapeOrScene != true)
        {
            scene.RotateScene(_angle);
        }
        else
        {
            scene.Rotate(_name, _angle);
        }
    }
}