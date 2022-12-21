using System.Drawing;

namespace Interpolation;

public class LagrangeInterpolator : CommonInterpolator
{
    private double[] _xValues;
    private double[] _yValues;

    public LagrangeInterpolator(double[] values, double[] xValues) : base(values)
    {
        if ((xValues != null && values != null) && (xValues.Length == values.Length))
        {
            _yValues = values;
            _xValues = xValues;
        }
        else
        {
            throw new ArgumentException("One of the arrays is empty or their lengths are not equal to each other.");
        }
    }

    public override double CalculateValue(double x)
    {
        double lagrangePol = 0;
        var xValues = (double[])_xValues.Clone();

        Array.Sort(xValues);

        for (var i = 0; i < xValues.Length - 1; i++)
        {
            if (xValues[i] == xValues[i + 1])
            {
                throw new ArgumentException("The values of x are not unique.");
            }
        }

        for (int i = 0; i < _xValues.Length; i++)
        {
            double basicsPol = 1;
            for (int j = 0; j < _xValues.Length; j++)
            {
                if (j != i)
                {
                    basicsPol *= (x - _xValues[j]) / (_xValues[i] - _xValues[j]);
                }
            }

            lagrangePol += basicsPol * _yValues[i];
        }

        return lagrangePol;
    }
}