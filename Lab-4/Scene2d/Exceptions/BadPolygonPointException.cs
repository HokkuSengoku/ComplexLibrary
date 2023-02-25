namespace Scene2d.Exceptions;

using System;

public class BadPolygonPointException : Exception
{
    public BadPolygonPointException(string message)
    {
        ExceptionMessage = message;
    }

    public string ExceptionMessage { get; }
}