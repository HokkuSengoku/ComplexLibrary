using Interpolation;

public class Program
{
    private const double SamplePoint = 3.0;

    public static void Main()
    {
        const double T = 3.0;
        Console.WriteLine("Enter the number of points to interpolate:");
        int.TryParse(Console.ReadLine(), out int size);
        double[] xValues = new double[size];
        double[] yValues = new double[size];
        Console.WriteLine($"Enter x values:");
        for (var i = 0; i < size; i++)
        {
            while (!double.TryParse(Console.ReadLine(), out xValues[i]))
            {
                Console.WriteLine("Incorrect value entered, try again.");
            }
        }

        Console.WriteLine($"Enter y values:");
        for (var i = 0; i < size; i++)
        {
            while (!double.TryParse(Console.ReadLine(), out yValues[i]))
            {
                Console.WriteLine("Incorrect value entered, try again.");
            }
        }

        var lagrange = new LagrangeInterpolator(yValues, xValues);
        var sinc = new SincInterpolation(yValues, T);

        object[] interpolators =
        {
            new StepInterpolator(yValues),
            new LinearInterpolator(yValues),
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