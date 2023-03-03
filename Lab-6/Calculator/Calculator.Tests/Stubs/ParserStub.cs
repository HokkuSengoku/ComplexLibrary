namespace Calculator.Tests.Stubs
{
    public class ParserStub : IParser
    {
        private readonly Operation _operation;

        public ParserStub(Operation operation)
        {
            _operation = operation;
        }

        public Operation Parse(string inputString)
        {
            return _operation;
        }
    }
}