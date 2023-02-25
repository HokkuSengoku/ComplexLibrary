namespace Scene2d.Exceptions
{
    using System;

    public class NameDoesAlreadyExistException : Exception
    {
        public NameDoesAlreadyExistException(string message)
        {
            ExceptionMessage = message;
        }

        public string ExceptionMessage { get; }
    }
}