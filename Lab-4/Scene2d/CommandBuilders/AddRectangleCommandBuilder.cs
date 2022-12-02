namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex("//regex to recognize all parameters of ad rectangle command");

        /* Should be set in AppendLine method */
        private IFigure _rectangle;

        /* Should be set in AppendLine method */
        private string _name;

        public bool IsCommandReady
        {
            get
            {
                /* "add rectangle" is a one-line command so it is always ready */
                return true;
            }
        }

        public void AppendLine(string line)
        {
            // check if line matches the RecognizeRegex
            var match = RecognizeRegex.Match(line);

            // if it matches select params of rectangle
            // name = ...
            // rectangle = new Rectangle(p1, p2)
            //
            // if it does not match throw BadFormatException
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _rectangle);
    }
}