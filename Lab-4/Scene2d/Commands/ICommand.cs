namespace Scene2d.Commands
{
    public interface ICommand
    {
        string FriendlyResultMessage { get; }

        void Apply(Scene scene);
    }
}