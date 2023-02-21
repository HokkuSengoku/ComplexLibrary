namespace Scene2d.CommandBuilders;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Scene2d.Commands;
using Scene2d.Exceptions;
using Scene2d.Figures;

public class GroupCommandBuilder : ICommandBuilder
{
    private static readonly Regex RecognizeRegex = new Regex(@"(group)|\s((\w+|[-])*),|(\s(\w+|[-])*)|(as(\w+|[-])*)");
    private static readonly Regex NameRegex = new Regex(@"(\w+|[-])*");
    private List<string> _compositeFigures = new List<string>();
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
        var match = RecognizeRegex.Match(line);
        var command = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        if (command[0] == "group" && command[command.Length - 2] == "as" && command.Length > 2)
        {
            _name = command[command.Length - 1];
            for (var i = 1; i < command.Length - 2; i++)
            {
                if (NameRegex.Match(command[i]).Success)
                {
                    _compositeFigures.Add(command[i]);
                }
                else
                {
                    throw new BadFormatException("bad format in line 41");
                }
            }
        }
        else
        {
            throw new BadFormatException("bad format in line 47");
        }
    }

    public ICommand GetCommand()
    {
        return new GroupCommand(_name, _compositeFigures);
    }
}