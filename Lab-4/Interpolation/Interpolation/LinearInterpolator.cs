namespace Interpolation;

internal class LinearInterpolator : CommonInterpolator
{
    public LinearInterpolator(double[] values) : base(values)
    {
    }

    public override double CalculateValue(double x)
    {
        var nmax = Values.Length - 1;

        if (nmax < 0)
        {
            return base.CalculateValue(x);
        }

        if (x < 0)
        {
            return Values[0];
        }

        var n = (int) x;

        return (n >= nmax) ? Values[nmax] : (Values[n] + (Values[n + 1] - Values[n]) * (x - n));
    }
}