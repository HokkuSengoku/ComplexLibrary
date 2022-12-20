namespace Interpolation;

internal class StepInterpolator : CommonInterpolator
{
    public StepInterpolator(double[] values) : base(values)
    {
    }

    public override double CalculateValue(double x)
    {
        if (Values.Length > 0)
        {
            return Values[GetSafeIndex((int) Math.Round(x))];
        }

        return base.CalculateValue(x);
    }

    private int GetSafeIndex(int index)
    {
        if (index < 0)
        {
            return 0;
        }

        if (index > Values.Length - 1)
        {
            return Values.Length - 1;
        }

        return index;
    }
}