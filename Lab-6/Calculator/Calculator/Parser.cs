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
            return null;
        }
    }
}