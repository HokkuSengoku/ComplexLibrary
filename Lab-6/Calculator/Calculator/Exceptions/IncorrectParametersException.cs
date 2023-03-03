namespace Calculator.Exceptions
{
    using System;

    public class IncorrectParametersException : Exception
    {
        // Выбрасываем это исключение когда метод IParser.Parse не находит
        // ни одного аргумента во входной строке.
    }
}
