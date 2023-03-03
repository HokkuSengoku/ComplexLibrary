namespace Calculator
{
    using System.Globalization;

    public class Evaluator
    {
        private readonly ICalculatorEngine _calculatorEngine;

        private readonly IParser _parser;

        public Evaluator(ICalculatorEngine calculatorEngine, IParser parser)
        {
            _calculatorEngine = calculatorEngine;
            _parser = parser;
        }

        public string Calculate(string inputString)
        {
            // todo: реализуйте метод Calculate().
            // Здесь вам нужно получить значение для выражения из inputString,
            // используя экземпляры классов Calculator и Parser
            // соответственно для распарсивания строки и вычисления выражения
            //
            // Обратите внимание на юнит-тесты для этого класса
            return null;
        }
    }
}
