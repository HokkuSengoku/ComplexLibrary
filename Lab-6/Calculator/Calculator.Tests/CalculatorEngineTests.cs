namespace Calculator.Tests
{
    using System;
    using Calculator.Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class CalculatorEngineTests
    {
        [Test]
        public void PerformOperation_Unary_ShouldCalculate()
        {
            var calculator = new CalculatorEngine();
            calculator.DefineOperation("++", x => x + 1);

            var operation = new Operation("++", new[] { 3.0 });

            var actual = calculator.PerformOperation(operation);

            Assert.That(actual, Is.EqualTo(4.0).Within(0.001), "Incorrect calculation result");
        }

        [Test]
        public void PerformOperation_Binary_ShouldCalculate()
        {
            var calculator = new CalculatorEngine();
            calculator.DefineOperation("*", (x, y) => x * y);

            var operation = new Operation("*", new[] { 5.0, 3.0 });
            var actual = calculator.PerformOperation(operation);

            Assert.That(actual, Is.EqualTo(15.0).Within(0.001), "Incorrect calculation result");
        }

        [Test]
        public void PerformOperation_Ternary_ShouldCalculate()
        {
            var calculator = new CalculatorEngine();
            calculator.DefineOperation("T5", (x, y, z) => Math.Min(x, Math.Min(y, z)));

            var operation = new Operation("T5", new[] { 2.0, 3.0, 6.0 });
            var actual = calculator.PerformOperation(operation);

            Assert.That(actual, Is.EqualTo(2.0).Within(0.001), "Incorrect calculation result");
        }

        [Test]
        public void DefineOperation_UnaryAlreadyExists_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("++", x => x + 1);

            Assert.Throws<AlreadyExistsOperationException>(() =>
            {
                calculator.DefineOperation("++", x => x + 1);
            });
        }

        [Test]
        public void DefineOperation_BinaryAlreadyExists_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("+", (x, y) => x + y);

            Assert.Throws<AlreadyExistsOperationException>(() =>
            {
                calculator.DefineOperation("+", (x, y) => x + y);
            });
        }

        [Test]
        public void DefineOperation_TernaryAlreadyExists_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("whatever", (x, y, z) => x + y + z);

            Assert.Throws<AlreadyExistsOperationException>(() =>
            {
                calculator.DefineOperation("whatever", (x, y, z) => x + y + z);
            });
        }

        [Test]
        public void DefineOperation_MultipleForSameSign_ShouldNotThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("whatever", (x) => x);
            calculator.DefineOperation("whatever", (x, y) => x + y);
            calculator.DefineOperation("whatever", (x, y, z) => x + y + z);
        }

        [Test]
        public void Calculate_OperationNotFound_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            Assert.Throws<NotFoundOperationException>(() =>
            {
                calculator.PerformOperation(new Operation("&&", new double[0]));
            });
        }

        [Test]
        public void Calculate_ArgumentOutOfRange_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            var sqrt = new Func<double, double>(
                x =>
                    {
                        if (x < 0)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        return Math.Sqrt(x);
                    });

            calculator.DefineOperation("sqrt", sqrt);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                calculator.PerformOperation(new Operation("sqrt", new double[] { -4 }));
            });
        }

        [Test]
        public void Calculate_UnaryParametersMismatch_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("++", x => x + 1);

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("++", new double[] { 1, 2 }));
            });

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("++", new double[] { 1, 2, 3 }));
            });
        }

        [Test]
        public void Calculate_BinaryParametersMismatch_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("+", (x, y) => x + y);

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("+", new double[] { 1 }));
            });

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("+", new double[] { 1, 2, 3 }));
            });
        }

        [Test]
        public void Calculate_TernaryParametersMismatch_ShouldThrow()
        {
            var calculator = new CalculatorEngine();

            calculator.DefineOperation("+", (x, y, z) => x + y + z);

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("+", new double[] { 1 }));
            });

            Assert.Throws<ParametersCountMismatchException>(() =>
            {
                calculator.PerformOperation(new Operation("+", new double[] { 1, 2 }));
            });
        }
    }
}
