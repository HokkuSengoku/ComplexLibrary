namespace Scene2d.CommandBuilders;

using System;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;

public class DeleteCommandBuilder : ICommandBuilder
{
    private static readonly Regex FigureOrGroupRegex = new Regex(@"((delete)\s(\((\w+||[-])*\)))");
    private static readonly Regex SceneRegex = new Regex(@"((delete)\s(\(scene\)))");
    private bool _isScene;
    private string _name;

    public bool IsCommandReady
    {
        get
        {
            return true;
        }
    }

    public void AppendLine(string line)
    {
        var separators = new char[] { ' ', '(', ')', ',' };
        if (SceneRegex.Match(line).Success)
        {
            var match = SceneRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _isScene = true;
        }
        else if (FigureOrGroupRegex.Match(line).Success)
        {
            var match = FigureOrGroupRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _name = command[1];
            _isScene = false;
        }
        else
        {
            throw new BadFormatException("Bad format error");
        }
    }

    public ICommand GetCommand()
    {
        throw new System.NotImplementedException();
    }
}