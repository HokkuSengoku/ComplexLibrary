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
    }
}
