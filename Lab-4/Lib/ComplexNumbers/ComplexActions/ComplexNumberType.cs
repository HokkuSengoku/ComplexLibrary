using System.Numerics;

namespace ComplexActions;

/// <summary>
/// A structure that defines the data type for complex numbers
/// and contains overloads of arithmetic operators for working with complex numbers.
/// </summary>
public struct ComplexNumberType : IEquatable<ComplexNumberType>
{
    /// <summary>
    /// Gets the real part of a complex number.
    /// </summary>
    public double RealValue { get; }

    /// <summary>
    /// Gets the imaginary part of a complex number.
    /// </summary>
    public double ImaginaryValue { get; }

    // Overloaded addition operators
    public static ComplexNumberType operator +(ComplexNumberType x1, ComplexNumberType x2)
    {
        return new ComplexNumberType(x1.RealValue + x2.RealValue, x1.ImaginaryValue + x2.ImaginaryValue);
    }

    public static ComplexNumberType operator +(double x1, ComplexNumberType x2)
    {
        return new ComplexNumberType(x1 + x2.RealValue, x2.ImaginaryValue);
    }

    public static ComplexNumberType operator +(ComplexNumberType x1, double x2)
    {
        return new ComplexNumberType(x1.RealValue + x2, x1.ImaginaryValue);
    }

    // Overloaded subtraction operators
    public static ComplexNumberType operator -(ComplexNumberType x1, ComplexNumberType x2)
    {
        return new ComplexNumberType(x1.RealValue - x2.RealValue, x1.ImaginaryValue - x2.ImaginaryValue);
    }

    public static ComplexNumberType operator -(double x1, ComplexNumberType x2)
    {
        return new ComplexNumberType(x1 - x2.RealValue, x2.ImaginaryValue);
    }

    public static ComplexNumberType operator -(ComplexNumberType x1, double x2)
    {
        return new ComplexNumberType(x1.RealValue - x2, x1.ImaginaryValue);
    }

    // Overloaded multiplication operators
    public static ComplexNumberType operator *(ComplexNumberType x1, ComplexNumberType x2)
    {
        return ComplexDo.ComplexMultiplication(x1, x2);
    }

    // Overloaded division operators
    public static ComplexNumberType operator /(ComplexNumberType x1, ComplexNumberType x2)
    {
        return ComplexDo.ComplexDiv(x1, x2);
    }

    // Overloading the comparison operator
    public static bool operator ==(ComplexNumberType x1, ComplexNumberType x2)
    {
        return Equals(x1, x2);
    }

    // Overloading the inequality operator
    public static bool operator !=(ComplexNumberType x1, ComplexNumberType x2)
    {
        return !(x1 == x2);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComplexNumberType"/> struct.
    /// </summary>
    /// <param name="real">The real part of a complex number.</param>
    /// <param name="imaginary">The imaginary part of a complex number.</param>
    public ComplexNumberType(double real, double imaginary)
    {
        RealValue = real;
        ImaginaryValue = imaginary;
    }

    /// <summary>
    /// A method for textual representation of a complex number.
    /// </summary>
    /// <returns>Returns a complex number as a string.</returns>
    public override string ToString()
    {
        if (ImaginaryValue > 0)
            return $"{RealValue} + {ImaginaryValue}i";
        return $"{RealValue} - {Math.Abs(ImaginaryValue)}i";
    }

    /// <summary>
    /// Redefined Equals method for complex numbers.
    /// </summary>
    /// <param name="obj">Accepts an object</param>
    /// <returns>The result of the comparison for matching the type ComplexNumberType</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if ((ComplexNumberType)obj is ComplexNumberType)
        {
            var complex = (ComplexNumberType)obj;
            if (Equals(complex))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    /// <summary>
    /// Redefined Equals method for complex numbers.
    /// </summary>
    /// <param name="other">Complex number.</param>
    /// <returns>The result of comparing complex numbers.</returns>
    public bool Equals(ComplexNumberType other)
    {
        if (other == null)
            return false;

        if (this.RealValue == other.RealValue && this.ImaginaryValue == other.ImaginaryValue)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Overriding the GetHashCode() method
    /// </summary>
    /// <returns>Returns the hash code of the real and imaginary parts of a complex number.</returns>
    public override int GetHashCode()
    {
        return RealValue.GetHashCode() ^ ImaginaryValue.GetHashCode();
    }
}