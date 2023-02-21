namespace Scene2d.CommandBuilders;

using System;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;

public class ReflectCommandBuilder : ICommandBuilder
{
    private static readonly Regex FigureRegex = new Regex(@"(reflect)\s((\(vertically\))|(\(horizontally\)))\s(\((\w+||[-])*\))");
    private static readonly Regex SceneRegex = new Regex(@"(reflect)\s((\(vertically\))|(\(horizontally\)))\s(\(scene\))");
    private bool _shapeOrScene;
    private string _name;
    private ReflectOrientation _orientation;

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
            _orientation = ReflectOrientationSelect(command[1]);
            _shapeOrScene = false;
        }
        else if (FigureRegex.Match(line).Success)
        {
            var match = FigureRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _orientation = ReflectOrientationSelect(command[1]);
            _name = command[2];
            _shapeOrScene = true;
        }
        else
        {
            throw new BadFormatException("Error in line 27/33: bad format");
        }
    }

    public ReflectOrientation ReflectOrientationSelect(string orientation)
    {
        var resultOrientation = ReflectOrientation.Vertical;
        switch (orientation)
        {
            case "vertically":
                resultOrientation = ReflectOrientation.Vertical;
                break;
            case "horizontally":
                resultOrientation = ReflectOrientation.Horizontal;
                break;
        }

        return resultOrientation;
    }

    public ICommand GetCommand()
    {
        if (_shapeOrScene != true)
        {
            return new ReflectCommand(_orientation, _shapeOrScene);
        }
        else
        {
            return new ReflectCommand(_orientation, _name, _shapeOrScene);
        }
    }
}