namespace Calculator.Tests
{
    using System.Collections.Generic;
    using Calculator;
    using Calculator.Tests.Stubs;
    using NUnit.Framework;

    [TestFixture]
    public class EvaluatorTest
    {
        [Test]
        public void Calculate_CorrectOperation_ShouldCallParserAndCalculatorEngine()
        {
            Operation operation = new Operation("test", new[] { 1d, 2d });
            IParser parser = new ParserStub(operation);
            ICalculatorEngine calculator = new CalculatorEngineStub(new Dictionary<Operation, double>
            {
                { operation, 42d },
            });

            var evaluator = new Evaluator(calculator, parser);

            Assert.That(evaluator.Calculate("any"), Is.EqualTo("42"));
        }
    }
}
