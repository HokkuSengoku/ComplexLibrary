namespace ComplexActions
{
    /// <summary>
    /// A partial class containing methods for working with complex numbers.
    /// </summary>
    public static partial class ComplexDo
    {
        /// <summary>
        /// A method for finding the sum of two complex numbers x, y.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Sum of (x,y) = Complex number.</returns>
        public static ComplexNumberType ComplexSum(ComplexNumberType x, ComplexNumberType y)
        {
            double real;
            double imaginary;
            real = x.RealValue + y.RealValue;
            imaginary = x.ImaginaryValue + y.ImaginaryValue;
            ComplexNumberType d = new ComplexNumberType(real, imaginary);

            return d;
        }

        /// <summary>
        /// A method for finding the sum of complex numbers.
        /// </summary>
        /// <param name="values">Array of complex numbers.</param>
        /// <returns>Sum of values = Complex number.</returns>
        public static ComplexNumberType ComplexSum(params ComplexNumberType[] values)
        {
            double real = 0;
            double imaginary = 0;
            foreach (var value in values)
            {
                real += value.RealValue;
                imaginary += value.ImaginaryValue;
            }

            ComplexNumberType d = new ComplexNumberType(real, imaginary);

            return d;
        }

        /// <summary>
        /// A method for finding the difference of complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Substract of (x,y) = Complex number.</returns>
        public static ComplexNumberType ComplexSubstract(ComplexNumberType x, ComplexNumberType y)
        {
            double real;
            double imaginary;
            real = x.RealValue - y.RealValue;
            imaginary = x.ImaginaryValue - y.ImaginaryValue;
            ComplexNumberType d = new ComplexNumberType(real, imaginary);

            return d;
        }

        /// <summary>
        /// A method for finding the product of two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Multiply of (x,y) = Complex number.</returns>
        public static ComplexNumberType ComplexMultiplication(ComplexNumberType x, ComplexNumberType y)
        {
            double real;
            double imaginary;
            real = (x.RealValue * y.RealValue) - (x.ImaginaryValue * y.ImaginaryValue);
            imaginary = (x.RealValue * y.ImaginaryValue) + (y.RealValue * x.ImaginaryValue);
            ComplexNumberType d = new ComplexNumberType(real, imaginary);
            return d;
        }

        /// <summary>
        /// A method for finding the product of a set of complex numbers.
        /// </summary>
        /// <param name="values">Array of Complex numbers.</param>
        /// <returns>Myltiply of values = complex numbers.</returns>
        public static ComplexNumberType ComplexMultiplication(params ComplexNumberType[] values)
        {
            ComplexNumberType q = new ComplexNumberType(values[0].RealValue, values[0].ImaginaryValue);
            for (var i = 1; i < values.Length; i++)
            {
                q = ComplexMultiplication(q, values[i]);
            }

            return q;
        }

        /// <summary>
        /// A method for finding the quotient of two complex numbers.
        /// </summary>
        /// <param name="x">Complex number x.</param>
        /// <param name="y">Complex number y.</param>
        /// <returns>Quotient of complex numbers (x,y) = complex number.</returns>
        public static ComplexNumberType ComplexDiv(ComplexNumberType x, ComplexNumberType y)
        {
            double real;
            double imaginary;
            real = ((x.RealValue * y.RealValue) + (x.ImaginaryValue * y.ImaginaryValue))
                   / (Math.Pow(y.RealValue, 2) + Math.Pow(y.ImaginaryValue, 2));
            imaginary = ((x.ImaginaryValue * y.RealValue) - (x.RealValue * y.ImaginaryValue))
                        / (Math.Pow(y.RealValue, 2) + Math.Pow(y.ImaginaryValue, 2));
            ComplexNumberType d = new ComplexNumberType(real, imaginary);
            return d;
        }
    }
}