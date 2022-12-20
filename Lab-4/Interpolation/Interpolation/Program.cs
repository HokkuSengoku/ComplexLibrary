using Interpolation;

public class Program
{
    private const double SamplePoint = 1.0;

    public static void Main()
    {
        
        double[] values = { 3.0, 1.0, 1.0 };
        double[] xValues = { 1.0, 0.0, -1.0 };
        double[] yValues = new double[xValues.Length];
        var lagrange = new LagrangeInterpolator(yValues, xValues);

        object[] interpolators =
        {
            new StepInterpolator(values),
            new LinearInterpolator(values), 
            lagrange
        };
        
        for (int i = 0; i < yValues.Length; i++)
        {
            yValues[i] = lagrange.ThePolynomial(xValues[i]);
        }

        Console.WriteLine("Calculating value at sample point: {0}", SamplePoint);

        foreach (var interpolator in interpolators)
        {
            if (interpolator is CommonInterpolator)
            {
                Console.WriteLine("Class {0}: Interpolated value is {1}",
                    interpolator.GetType().Name,
                    (interpolator as CommonInterpolator).CalculateValue(SamplePoint));
            }
        }
    }
}