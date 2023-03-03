namespace Calculator.Exceptions
{
    using System;

    public class NotFoundOperationException : Exception
    {
        // Выбрасываем это исключение когда в методе ICalculatorEngine.PerformOperation вызывается
        // несуществующая операция.
    }
}
