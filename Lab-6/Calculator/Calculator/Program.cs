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
                calculator.DefineOperation("-", (a, b, c) => a - b - c);
                calculator.DefineOperation("+", a => +a);
                calculator.DefineOperation("+", (a, b) => a + b);
                calculator.DefineOperation("+", (a, b, c) => a + b + c);
                calculator.DefineOperation("/", a => a / a);
                calculator.DefineOperation("/", (a, b) => a / b);
                calculator.DefineOperation("/", (a, b, c) => a / b / c);
                calculator.DefineOperation("++", a => ++a);

                // обратите внимание: подставляется напрямую метод класса Math
                // это эквивалентно calculator.DefineOperation("^", (x, y) => Math.Pow(x, y)), но лаконичнее
                calculator.DefineOperation("^", Math.Pow);

                // ... определите остальные операции здесь ...
            }
            catch (AlreadyExistsOperationException)
            {
                Console.WriteLine("This operation already exists in the calculator");
            }
            catch (NotFoundOperationException)
            {
                Console.WriteLine("The operation does not exist");
            }
            catch (IncorrectParametersException)
            {
                Console.WriteLine("Invalid parameters");
            }
            catch (ParametersCountMismatchException)
            {
                Console.WriteLine("Invalid number of parameters");
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
                catch (AlreadyExistsOperationException)
                {
                    Console.WriteLine("This operation already exists in the calculator");
                }
                catch (NotFoundOperationException)
                {
                    Console.WriteLine("The operation does not exist");
                }
                catch (IncorrectParametersException)
                {
                    Console.WriteLine("Invalid parameters");
                }
                catch (ParametersCountMismatchException)
                {
                    Console.WriteLine("Invalid number of parameters");
                }

                // todo: кажется здесь мы "отловили" только одно
                // исключение NotFoundOperationException,
                // не забудьте отловить оставшиеся
            }
        }
    }
}
