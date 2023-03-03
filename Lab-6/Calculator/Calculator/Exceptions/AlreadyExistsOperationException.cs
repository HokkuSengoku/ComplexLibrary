namespace Calculator.Exceptions
{
    using System;

    public class AlreadyExistsOperationException : Exception
    {
        // Выбрасываем это исключение когда в методе ICalculatorEngine.DefineOperations регистрируется
        // существующая операция с тем же количеством параметров.
    }
}
