namespace Calculator
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Calculator.Exceptions;

    public class Parser : IParser
    {
        public Operation Parse(string inputString)
        {
            // todo: реализуйте метод Parse().
            // этод метод парсит строку inputString и возвращает объект Operation
            // Формат строки: {имя_операции} {параметр1} ... {параметрN}
            // Обратите внимание:
            // предварительно повторяющиеся пробелы и пробелы в начале и в конце нужно игнорировать
            //
            // Если что-то пойдет не так (например, из строки нельзя выделить
            // знак операции и как минимум один параметр), не забудьте сгенерировать
            // соответствующее исключение из папки Exceptions
            //
            // Обратите внимание на юнит-тесты для этого класса
            var command =
                inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (command.Length < 2)
            {
                throw new IncorrectParametersException();
            }

            var sign = command[0];
            double[] parameters = new double[command.Length - 1];
            for (var i = 1; i < command.Length; i++)
            {
                double.TryParse(command[i], out parameters[i - 1]);
            }

            Operation operation = new Operation(sign, parameters);
            return operation;
        }
    }
}