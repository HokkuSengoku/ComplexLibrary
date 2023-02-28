namespace Scene2d.Commands
{
    using Scene2d.Figures;

    public class AddFigureCommand : ICommand
    {
        private readonly string _name;

        private readonly IFigure _figure;

        public AddFigureCommand(string name, IFigure figure)
        {
            _name = name;
            _figure = figure;
        }

        public string FriendlyResultMessage
        {
            get { return "Added figure " + _name + " of type " + _figure.GetType().Name; }
        }

        public void Apply(Scene scene)
        {
            scene.AddFigure(_name, _figure);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AddFigureCommand);
        }

        public bool Equals(AddFigureCommand command)
        {
            return command != null &&
                   _name == command._name &&
                   Equals(_figure, command._figure);
        }
    }
}
