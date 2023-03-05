namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using Calculator.Exceptions;

    public class CalculatorEngine : ICalculatorEngine
    {
        private Dictionary<string, Func<double, double, double>> _doubleParameters = new Dictionary<string, Func<double, double, double>>();

        private Dictionary<string, Func<double, double>>
            _oneParameters = new Dictionary<string, Func<double, double>>();

        private Dictionary<string, Func<double, double, double, double>> _threeParameters =
            new Dictionary<string, Func<double, double, double, double>>();

        public double PerformOperation(Operation operation)
        {
            var operationSign = operation.Sign;

            // Сейчас наш калькулятор знает три операции.
            // Необходимо добавить возможность “обучения” калькулятора новым операциям.
            // Пример обучения есть в классе Program.cs
            // Очевидно, что от Switch-а придется избавиться.
            // Достойной альтернативой Switch-у может быть, например,
            // словарь в котором ключём будет строка (знак операции),
            // а значением будет делегат или лямбда-выражение.
            //
            // todo: Переработайте метод PerformOperation()
            //
            // предлагаемая реализация с помощью cловаря (словарей):
            // ищем знак операции в словарях
            // если находим, выполняем найденную лямбду с помощью параметров,
            //   передаваемых в operation
            //
            // Если что-то пойдет не так, не забудьте сгенерировать
            // соответствующее исключение из папки Exceptions
            //
            // Обратите внимание на юнит-тесты для этого класса
            if (operation.Parameters.Length < 1)
            {
                throw new NotFoundOperationException();
            }

            if (_oneParameters.ContainsKey(operationSign) && operation.Parameters.Length == 1)
            {
                return _oneParameters[operationSign](operation.Parameters[0]);
            }

            if (_doubleParameters.ContainsKey(operationSign) && operation.Parameters.Length == 2)
            {
                return _doubleParameters[operationSign](operation.Parameters[0], operation.Parameters[1]);
            }

            if (_threeParameters.ContainsKey(operationSign) && operation.Parameters.Length == 3)
            {
                return _threeParameters[operationSign](operation.Parameters[0], operation.Parameters[1], operation.Parameters[2]);
            }
            else
            {
                throw new ParametersCountMismatchException();
            }
        }

        // todo: реализуйте методы DefineOperation().
        // метод должен добавить новую операцию в калькулятор
        //
        // предлагаемая реализация с помощью cловаря (словарей):
        //  - проверка на существование операции
        //  - добавление новой операции в словарь
        // Если что-то пойдет не так, не забудьте сгенерировать
        // соответствующее исключение из папки Exceptions
        //
        // Обратите внимание на юнит-тесты для этого класса
        public void DefineOperation(string sign, Func<double, double, double, double> body)
        {
            if (!_threeParameters.ContainsKey(sign))
            {
                _threeParameters[sign] = body;
            }
            else
            {
                throw new AlreadyExistsOperationException();
            }
        }

        public void DefineOperation(string sign, Func<double, double, double> body)
        {
            if (!_doubleParameters.ContainsKey(sign))
            {
                _doubleParameters[sign] = body;
            }
            else
            {
                throw new AlreadyExistsOperationException();
            }
        }

        public void DefineOperation(string sign, Func<double, double> body)
        {
            if (!_oneParameters.ContainsKey(sign))
            {
                _oneParameters[sign] = body;
            }
            else
            {
                throw new AlreadyExistsOperationException();
            }
        }
    }
}
