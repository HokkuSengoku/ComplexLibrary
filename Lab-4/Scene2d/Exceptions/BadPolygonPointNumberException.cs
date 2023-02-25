namespace Scene2d.Exceptions
{
    using System;

    public class BadPolygonPointNumberException : Exception
    {
        public BadPolygonPointNumberException(string message)
        {
            ExceptionMessage = message;
        }

        public string ExceptionMessage { get; }
    }
}