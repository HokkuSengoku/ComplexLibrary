using Interpolation;

public class Program
{
    private const double SamplePoint = 1.0;

    public static void Main()
    {
        double[] values = { 0.000, 2.187, 3.188 };
        const double T = 3.0;
        Console.WriteLine("Enter the number of points to interpolate:");
        int.TryParse(Console.ReadLine(), out int size);
        double[] xValues = new double[size];
        double[] yValues = new double[size];
        Console.WriteLine($"Enter x values:");
        for (var i = 0; i < size; i++)
        {
            xValues[i] = Convert.ToDouble(Console.ReadLine());
        }

        Console.WriteLine($"Enter y values:");
        for (var i = 0; i < size; i++)
        {
            yValues[i] = Convert.ToDouble(Console.ReadLine());
        }

        var lagrange = new LagrangeInterpolator(yValues, xValues);
        var sinc = new SincInterpolation(values, T);

        object[] interpolators =
        {
            new StepInterpolator(values),
            new LinearInterpolator(values),
            lagrange,
            sinc,
        };

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