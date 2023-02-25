namespace Scene2d.Exceptions;

using System;

public class BadCircleRadiusException : Exception
{
    public BadCircleRadiusException(string message)
    {
        ExceptionMessage = message;
    }

    public string ExceptionMessage { get; }
}