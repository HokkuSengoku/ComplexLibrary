using Interpolation;
using NUnit.Framework;

namespace InterpolationTest;


public class Tests
{
    [TestCase(1.0, 1.0, new double[] {1.0, 0.0, -1.0}, new double[] {1.0, 1.0, 3.0})]
    [TestCase(21.0, 5.0, new double[] {5.0, 0.0, -5.0}, new double[] {21.0, 1.0, 31.0})]
    [TestCase(91.0, 10.0, new double[] {1.0, 0.0, -1.0}, new double[] {1.0, 1.0, 3.0})]
    [TestCase(173.5, 10.0, new double[] {1.0, 0.0, -1.0, 5.0}, new double[] {1.0, 1.0, 3.0, 31.0})]
    [TestCase(13.0, 3.0, new double[] {1.0, 0.0, -1.0, 2.0, 3.0}, new double[] {1.0, 1.0, 3.0, 7.0, 13.0})]
    public void LagrangeTest(double expected, double SamplePoint, double[] xValues, double[] yValues)
    {
        var actual = new LagrangeInterpolator(yValues, xValues).CalculateValue(SamplePoint);
        Assert.AreEqual(expected, actual);
    }

    [TestCase(2.187, 3, new double[] {0.0, 2.187, 3.188, 3.486}, 3)]
    [TestCase(2.187, 3, new double[] {0.0, 2.187, 3.188}, 3)]
    public void SincTest(double expected, double SamplePoint, double[] values, double T)
    {
        var actual = new SincInterpolation(values, T).CalculateValue(SamplePoint);
        Assert.AreEqual(expected, actual, 0.000000000000001);
    }
}