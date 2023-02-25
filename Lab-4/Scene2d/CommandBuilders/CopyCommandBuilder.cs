namespace Scene2d.CommandBuilders;

using System;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;

public class CopyCommandBuilder : ICommandBuilder
{
    private static readonly Regex FigureOrGroupRegex = new Regex(@"((copy)\s(\((\w+||[-])*\))\s(to)\s(\w+||[-])*)");
    private static readonly Regex SceneRegex = new Regex(@"((copy)\s(\(scene\))\s(to)\s(\w+||[-])*)");
    private string _name;
    private string _nameTo;
    private bool _isScene;

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
            _nameTo = command[3];
            _isScene = true;
        }
        else if (FigureOrGroupRegex.Match(line).Success)
        {
            var match = FigureOrGroupRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _name = command[1];
            _nameTo = command[3];
            _isScene = false;
        }
        else
        {
            throw new BadFormatException("Bad format error");
        }
    }

    public ICommand GetCommand()
    {
        if (_isScene)
        {
            return new CopyCommand(_nameTo, _isScene);
        }
        else
        {
            return new CopyCommand(_name, _nameTo, _isScene);
        }
    }
}