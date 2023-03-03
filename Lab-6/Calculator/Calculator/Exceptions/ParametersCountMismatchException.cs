namespace Calculator.Exceptions
{
    using System;

    public class ParametersCountMismatchException : Exception
    {
        // Выбрасываем это исключение когда в методе ICalculatorEngine.PerformOperation вызывается
        // операция с неверным числом параметров.
    }
}