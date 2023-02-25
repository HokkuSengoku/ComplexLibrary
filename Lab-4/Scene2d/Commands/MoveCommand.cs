namespace Scene2d.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly string _name;

        private readonly ScenePoint _vector;

        private readonly bool _shapeOrScene;

        public MoveCommand(string name, ScenePoint vector, bool shapeOrScene)
        {
            _name = name;
            _vector = vector;
            _shapeOrScene = shapeOrScene;
        }

        public MoveCommand(ScenePoint vector, bool shapeOrScene)
        {
            _vector = vector;
            _shapeOrScene = shapeOrScene;
        }

        public string FriendlyResultMessage
        {
            get
            {
                if (_shapeOrScene != true)
                {
                    return "Moved Scene";
                }
                else
                {
                    return $"Moved figure {_name} by vector  ({_vector.X}, {_vector.Y})";
                }
            }
        }

        public void Apply(Scene scene)
        {
            if (_shapeOrScene != true)
            {
                scene.MoveScene(_vector);
            }
            else
            {
                scene.Move(_name, _vector);
            }
        }
    }
}