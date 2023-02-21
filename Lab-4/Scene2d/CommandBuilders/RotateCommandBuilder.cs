namespace Scene2d.CommandBuilders;

using System;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;

public class RotateCommandBuilder : ICommandBuilder
{
    private static readonly Regex FigureRegex = new Regex(@"((rotate)\s(\((\w+||[-])*\))\s[+-]?\d*)");
    private static readonly Regex SceneRegex = new Regex(@"((rotate)\s(\(scene\))\s[+-]?\d*)");
    private static readonly Regex NotNameScene = new Regex(@"(\(scene\))");
    private string _name;
    private bool _shapeOrScene;
    private double _angle;

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

        if (FigureRegex.Match(line).Success && !NotNameScene.Match(line).Success)
        {
            var match = FigureRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            _name = command[1];
            double.TryParse(command[2], out _angle);
            _shapeOrScene = true;
        }
        else if (SceneRegex.Match(line).Success)
        {
            var match = SceneRegex.Match(line);
            var command = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            double.TryParse(command[2], out _angle);
            _shapeOrScene = false;
        }
        else
        {
            throw new BadFormatException("Error in line 29/37: bad format");
        }
    }

    public ICommand GetCommand()
        {
            if (_shapeOrScene != true)
            {
                return new RotateCommand(_angle, _shapeOrScene);
            }
            else
            {
                return new RotateCommand(_name, _angle, _shapeOrScene);
            }
        }
    }