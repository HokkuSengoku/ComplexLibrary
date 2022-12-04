namespace ComplexActions;

/// <summary>
/// A partial class containing methods for working with complex numbers.
/// </summary>
public static partial class ComplexDo
{
    /// <summary>
    /// A method for finding the roots of the Nth degree of a complex number.
    /// </summary>
    /// <param name="x">Complex number x.</param>
    /// <param name="powCounter">Degree indicator.</param>
    /// <returns>A list of roots of a complex number.</returns>
    public static List<ComplexNumberType> GetNComplexRoots(ComplexNumberType x, int powCounter)
    {
        double arg = ComplexArg(x);
        double abs = ComplexAbs(x);
        double sqrtNAbs = 0.0;

        if (powCounter <= 0)
        {
            throw new ArgumentException($"The degree value must not be negative or equal to zero. powCounter = {powCounter}");
        }

        if (powCounter % 2 == 0 && abs > 0)
        {
            sqrtNAbs = Math.Pow(abs, 1.0 / powCounter);
        }
        else if (powCounter % 2 != 0)
        {
            sqrtNAbs = Math.Pow(abs, 1.0 / powCounter);
        }

        double real;
        double imaginary;
        List<ComplexNumberType> roots = new List<ComplexNumberType>();

        for (var i = 0; i < powCounter; i++)
        {
            real = sqrtNAbs * Math.Cos((arg + (Math.PI * 2 * i)) / powCounter);
            imaginary = sqrtNAbs * Math.Sin((arg + (Math.PI * 2 * i)) / powCounter);
            roots.Add(new ComplexNumberType(real, imaginary));
        }

        var k = 0;
        foreach (var root in roots)
        {
            Console.WriteLine($"k = {k}, root = {root.ToString()}");
            k++;
        }

        return roots;
    }

    /// <summary>
    /// A method for finding the degree of a complex number.
    /// </summary>
    /// <param name="x">Complex number x.</param>
    /// <param name="powCounter">Degree indicator.</param>
    /// <returns>A complex number in the power of N.</returns>
    public static ComplexNumberType ComplexPowTrigForm(ComplexNumberType x, int powCounter)
    {
        double arg = ComplexArg(x);
        double abs = ComplexAbs(x);

        double real = Math.Pow(abs, powCounter) * Math.Cos(arg * powCounter);
        double imaginary = Math.Pow(abs, powCounter) * Math.Sin(arg * powCounter);
        ComplexNumberType d = new ComplexNumberType(real, imaginary);
        return d;
    }

    /// <summary>
    /// A method for finding the modulus of a complex number.
    /// </summary>
    /// <param name="x">Complex number x.</param>
    /// <returns>The value of the modulus of a complex number.</returns>
    public static double ComplexAbs(ComplexNumberType x)
    {
        double absResult;

        absResult = Math.Sqrt((x.RealValue * x.RealValue) + (x.ImaginaryValue * x.ImaginaryValue));
        return absResult;
    }

    /// <summary>
    /// A method for finding the argument of a complex number.
    /// </summary>
    /// <param name="x">Complex number x.</param>
    /// <returns>The argument of a complex number.</returns>
    public static double ComplexArg(ComplexNumberType x)
    {
        double arg = 0;
        if (x.RealValue > 0 && x.ImaginaryValue > 0) 
            arg = Math.Atan(x.ImaginaryValue / x.RealValue);
        else if (x.RealValue > 0 && x.ImaginaryValue < 0)
            arg = -Math.Atan(x.ImaginaryValue / x.RealValue);
        else if (x.RealValue < 0 && x.ImaginaryValue > 0)
            arg = Math.PI - Math.Atan(x.ImaginaryValue / x.RealValue);
        else if (x.RealValue < 0 && x.ImaginaryValue < 0)
            arg = -Math.PI + Math.Atan(x.ImaginaryValue / x.RealValue);
        else if (x.RealValue == 0 && x.ImaginaryValue > 0)
            arg = Math.PI / 2;
        else if (x.RealValue == 0 && x.ImaginaryValue < 0)
            arg = -(Math.PI / 2);
        else if (x.RealValue > 0 && x.ImaginaryValue == 0)
            arg = 0;
        else if (x.RealValue < 0 && x.ImaginaryValue == 0)
            arg = Math.PI;
        return arg;
    }
}