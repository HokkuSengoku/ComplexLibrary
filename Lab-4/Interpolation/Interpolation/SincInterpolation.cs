namespace Interpolation;

public class SincInterpolation : CommonInterpolator
{
    private double[] Values { get; }

    private double _T;

    public SincInterpolation(double[] values, double T) : base(values)
    {
        if (values != null)
        {
            Values = values;
            _T = T;
        }
        else
        {
            throw new ArgumentException("The array is empty.");
        }
    }

    public override double CalculateValue(double x)
    {
        double sincResult = 0;
        double sincInside;
        for (var i = 0; i < Values.Length; i++)
        {
            sincInside = (x - i * _T) / _T;
            sincResult += Values[i] * Sinc(sincInside);
        }

        return sincResult;
    }

    public static double Sinc(double x)
    {
        if (x == 0)
            return 1.0;
        return Math.Sin(Math.PI * x) / (Math.PI * x);
    }
}