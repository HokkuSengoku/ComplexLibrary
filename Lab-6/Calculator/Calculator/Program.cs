namespace Calculator
{
    using System;

    using Calculator.Exceptions;

    public static class Program
    {
        public static void Main()
        {
            ICalculatorEngine calculator = new CalculatorEngine();
            IParser parser = new Parser();

            try
            {
                // пример определяемых операций
                // (сейчас их добавление в калькулятор не реализовано - это ваша задача)
                var sqrt = new Func<double, double>(x => // пример многострочной лямбды
                {
                    if (x < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    return Math.Sqrt(x);
                });

                calculator.DefineOperation("sqrt", sqrt);

                // можно использовать одинаковое имя для операций с разным количеством аргументов
                calculator.DefineOperation("-", a => -a);
                calculator.DefineOperation("-", (a, b) => a - b);

                // обратите внимание: подставляется напрямую метод класса Math
                // это эквивалентно calculator.DefineOperation("^", (x, y) => Math.Pow(x, y)), но лаконичнее
                calculator.DefineOperation("^", Math.Pow);

                // ... определите остальные операции здесь ...
            }
            catch (AlreadyExistsOperationException)
            {
                Console.WriteLine("This operation already exists in the calculator");
            }

            var evaluator = new Evaluator(calculator, parser);
            Console.WriteLine("Please enter expressions: ");

            while (true)
            {
                string line = Console.ReadLine();
                if (line == null || line.Trim().Length == 0)
                {
                    break;
                }

                try
                {
                    Console.WriteLine(evaluator.Calculate(line));
                }
                catch (NotFoundOperationException)
                {
                    // todo сообщение об ошибке
                }

                // todo: кажется здесь мы "отловили" только одно
                // исключение NotFoundOperationException,
                // не забудьте отловить оставшиеся
            }
        }
    }
}
