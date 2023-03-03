namespace Calculator
{
    public class Operation
    {
        public Operation(string sign, double[] parameters)
        {
            Sign = sign;
            Parameters = parameters;
        }

        public string Sign { get; }

        public double[] Parameters { get; }
    }
}
