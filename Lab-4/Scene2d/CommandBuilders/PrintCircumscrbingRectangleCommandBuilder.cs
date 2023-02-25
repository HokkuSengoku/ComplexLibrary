namespace Scene2d.CommandBuilders;

using System;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;

public class PrintCircumscrbingRectangleCommandBuilder : ICommandBuilder
{
    private static readonly Regex GroupOrFigureRegex =
        new Regex(@"((print circumscribing rectangle for)\s(\((\w+|[-])*\)))");

    private static readonly Regex SceneRegex = new Regex(@"((print circumscribing rectangle for)\s(\(scene\)))");
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
        else if (GroupOrFigureRegex.Match(line).Success)
        {
            var match = GroupOrFigureRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _name = command[4];
            _isScene = false;
        }
        else
        {
            throw new BadFormatException("Error in line 42: bad format");
        }
    }

    public ICommand GetCommand()
    {
        if (_isScene)
        {
            return new PrintCircumscribingRectangleCommand(_isScene);
        }
        else
        {
            return new PrintCircumscribingRectangleCommand(_name, _isScene);
        }
    }
}