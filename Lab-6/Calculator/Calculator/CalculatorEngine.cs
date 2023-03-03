namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using Calculator.Exceptions;

    public class CalculatorEngine : ICalculatorEngine
    {
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
            switch (operationSign)
            {
                case "+":
                    return operation.Parameters[0] + operation.Parameters[1];
                case "++":
                    return operation.Parameters[0] + 1;
                case "*":
                    return operation.Parameters[0] * operation.Parameters[1];
            }

            return 0;
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
        }

        public void DefineOperation(string sign, Func<double, double, double> body)
        {
        }

        public void DefineOperation(string sign, Func<double, double> body)
        {
        }
    }
}
