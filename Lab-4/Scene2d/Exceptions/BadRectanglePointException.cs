namespace Scene2d.Exceptions;

using System;

public class BadRectanglePointException : Exception
{
    public BadRectanglePointException(string message)
    {
        ExceptionMessage = message;
    }

    public string ExceptionMessage { get; }
}