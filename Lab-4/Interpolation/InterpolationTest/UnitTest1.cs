using Interpolation;

namespace InterpolationTest;


public class Tests
{
    [TestCase(3.0, 1.0, new double[] {1.0, 0.0, -1.0}, new double[] {1.0, 1.0, 3.0})]
    [TestCase(31.0, 5.0, new double[] {5.0, 0.0, -5.0}, new double[] {21.0, 1.0, 31.0})]
    [TestCase(111.0, 10.0, new double[] {1.0, 0.0, -1.0}, new double[] {1.0, 1.0, 3.0})]
    [TestCase(111.0, 10.0, new double[] {1.0, 0.0, -1.0, 5.0}, new double[] {1.0, 1.0, 3.0, 31.0})]
    [TestCase(13.0, 3.0, new double[] {1.0, 0.0, -1.0, 2.0, 3.0}, new double[] {1.0, 1.0, 3.0, 7.0, 13.0})]
    public void LagrangeTest(double expected, double SamplePoint, double[] xValues, double[] yValues)
    {
        var actual = new LagrangeInterpolator(yValues, xValues).CalculateValue(SamplePoint);
        Assert.AreEqual(expected, actual);
    }
}