namespace Interpolation;

public class CommonInterpolator
{
    private double[] _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonInterpolator"/> class.
    /// </summary>
    /// <param name="values"></param>
    public CommonInterpolator(double[] values)
    {
        _values = values;
    }

    public virtual double CalculateValue(double x)
    {
        return 0;
    }

    protected double[] Values
    {
        get { return _values; }
    }
}