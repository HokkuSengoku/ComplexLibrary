using System.Drawing;

namespace Interpolation;

public class LagrangeInterpolator : CommonInterpolator
{
    private double[] _xValues;
    private double[] _yValues;

    public LagrangeInterpolator(double[] values, double[] xValues) : base(values)
    {
        _yValues = values;
        _xValues = xValues;
    }

    public override double CalculateValue(double x)
    {
        double lagrangePol = 0;
        Array.Sort(_xValues);
        Array.Sort(_yValues);
        for (var i = 0; i < _xValues.Length - 1; i++)
        {
            if (_xValues[i] == _xValues[i + 1])
            {
                throw new ArgumentException("Neighboring points have the same value.");
            }
        }

        if ((_xValues != null && _yValues != null) && (_xValues.Length == _yValues.Length))
        {
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
        }
        else
        {
            throw new ArgumentException("One of the arrays is empty or their lengths are not equal to each other.");
        }

        return lagrangePol;
    }

    public double ThePolynomial(double x)
    {
        return x * x + x + 1;
    }
}