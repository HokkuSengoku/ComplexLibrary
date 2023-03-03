namespace Calculator
{
    using System;

    public interface ICalculatorEngine
    {
        double PerformOperation(Operation operation);

        void DefineOperation(string sign, Func<double, double, double, double> body);

        void DefineOperation(string sign, Func<double, double, double> body);

        void DefineOperation(string sign, Func<double, double> body);
    }
}