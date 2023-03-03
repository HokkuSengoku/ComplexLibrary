namespace Calculator.Tests
{
    using Calculator;
    using Calculator.Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void Parse_UnaryOperation_ShouldParse()
        {
            var expected = new Operation("++", new[] { 3.0 });

            var parser = new Parser();
            var actual = parser.Parse(" ++  3");

            Assert.That(actual.Sign, Is.EqualTo(expected.Sign), "Sign incorrectly defined");
            Assert.That(actual.Parameters, Has.Length.EqualTo(1), "Parameters count mismatch");
            Assert.That(actual.Parameters[0], Is.EqualTo(expected.Parameters[0]).Within(0.001), "First option incorrectly defined");
        }

        [Test]
        public void Parse_BinaryOperation_ShouldParse()
        {
            var expected = new Operation("-", new[] { 5.0, 3.0 });

            var parser = new Parser();
            var actual = parser.Parse("- 5   3 ");

            Assert.That(actual.Sign, Is.EqualTo(expected.Sign), "Sign incorrectly defined");
            Assert.That(actual.Parameters, Has.Length.EqualTo(2), "Parameters count mismatch");
            Assert.That(actual.Parameters[0], Is.EqualTo(expected.Parameters[0]).Within(0.001), "First option incorrectly defined");
            Assert.That(actual.Parameters[1], Is.EqualTo(expected.Parameters[1]).Within(0.001), "Second option incorrectly defined");
        }

        [Test]
        public void Parse_TernaryOperation_ShouldParse()
        {
            var expected = new Operation("T5", new[] { 5.0, 3.0, 4.0 });

            var parser = new Parser();
            var actual = parser.Parse(" T5 5 3   4 ");

            Assert.That(actual.Sign, Is.EqualTo(expected.Sign), "Sign incorrectly defined");
            Assert.That(actual.Parameters, Has.Length.EqualTo(3), "Parameters count mismatch");
            Assert.That(actual.Parameters[0], Is.EqualTo(expected.Parameters[0]).Within(0.001), "First option incorrectly defined");
            Assert.That(actual.Parameters[1], Is.EqualTo(expected.Parameters[1]).Within(0.001), "Second option incorrectly defined");
            Assert.That(actual.Parameters[2], Is.EqualTo(expected.Parameters[2]).Within(0.001), "Third option incorrectly defined");
        }

        [Test]
        public void Parse_WrongInput_ShouldThrow()
        {
            var parser = new Parser();

            Assert.Throws<IncorrectParametersException>(() =>
            {
                parser.Parse(" +");
            });
        }
    }
}
