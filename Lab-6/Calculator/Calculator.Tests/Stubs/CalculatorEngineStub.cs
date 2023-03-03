namespace Calculator.Tests.Stubs
{
    using System;
    using System.Collections.Generic;

    public class CalculatorEngineStub : ICalculatorEngine
    {
        private readonly IDictionary<Operation, double> _results;

        public CalculatorEngineStub(IDictionary<Operation, double> results)
        {
            _results = results;
        }

        public double PerformOperation(Operation operation)
        {
            if (_results.ContainsKey(operation))
            {
                return _results[operation];
            }

            throw new Exception("Operation not found: " + operation.Sign);
        }

        public void DefineOperation(string sign, Func<double, double, double, double> body)
        {
            throw new NotSupportedException("Calculator Engine Stub does not support operations definitions");
        }

        public void DefineOperation(string sign, Func<double, double, double> body)
        {
            throw new NotSupportedException("Calculator Engine Stub does not support operations definitions");
        }

        public void DefineOperation(string sign, Func<double, double> body)
        {
            throw new NotSupportedException("Calculator Engine Stub does not support operations definitions");
        }
    }
}