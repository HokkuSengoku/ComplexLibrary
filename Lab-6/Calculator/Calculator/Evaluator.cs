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
            var operation = _parser.Parse(inputString);
            var result = _calculatorEngine.PerformOperation(operation);
            return result.ToString();
        }
    }
}
