namespace Scene2d.Exceptions
{
using System;

public class BadNameException : Exception
{
    public BadNameException(string message)
        {
            ExceptionMessage = message;
        }

    public string ExceptionMessage { get; }
    }
}