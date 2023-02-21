namespace Scene2d.Commands;

public class ReflectCommand : ICommand
{
    private readonly string _name;

    private readonly ReflectOrientation _orientation;

    private readonly bool _shapeOrScene;

    public ReflectCommand(ReflectOrientation orientation, string name, bool shapeOrScene)
    {
        _orientation = orientation;
        _name = name;
        _shapeOrScene = shapeOrScene;
    }

    public ReflectCommand(ReflectOrientation orientation, bool shapeOrScene)
    {
        _orientation = orientation;
        _shapeOrScene = shapeOrScene;
    }

    public string FriendlyResultMessage
    {
        get
        {
            if (_shapeOrScene != true)
            {
                return $"Reflected Scene {_orientation}";
            }
            else
            {
                return "Reflected figure " + _name + " " + _orientation;
            }
        }
    }

    public void Apply(Scene scene)
    {
        if (_shapeOrScene != true)
        {
            scene.ReflectScene(_orientation);
        }
        else
        {
            scene.Reflect(_name, _orientation);
        }
    }
}