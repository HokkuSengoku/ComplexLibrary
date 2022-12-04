// See https://aka.ms/new-console-template for more information

using System.Numerics;
using ComplexActions;



ComplexDo.ComplexSum(new ComplexNumberType(1.0, 2.0), new ComplexNumberType(1.0, 2.0), new ComplexNumberType(1.0, 2.0));
Console.WriteLine(ComplexDo.ComplexSum(new ComplexNumberType(1.0, 2.0), new ComplexNumberType(1.0, 2.0), new ComplexNumberType(1.0, 2.0)).ToString());

/*Console.WriteLine(ComplexDo.ComplexMultiplication(new ComplexNumberType(2.0, 3.0), new ComplexNumberType(-1.0, 1.0)).ToString());
Console.WriteLine(new ComplexNumberType(2.0, -5.0).ToString());
Console.WriteLine(new ComplexNumberType(2.0, 1.0).ToString());
Console.WriteLine(ComplexDo.ComplexDiv(new ComplexNumberType(3.0, -1.0), new ComplexNumberType(-5.0, 2.0)).ToString());*/

Console.WriteLine(Complex.Multiply(new Complex(3.0, -1.0), new Complex(-5.0, 2.0)));
Console.WriteLine(ComplexDo.ComplexMultiplication(new ComplexNumberType(3.0, -1.0), new ComplexNumberType(-5.0, 2.0)).ToString());
Console.WriteLine(Complex.Multiply(new Complex(-13.0, 11.0), new Complex(-5.0, 2.0)));

Console.WriteLine(ComplexDo.ComplexMultiplication(new ComplexNumberType(3.0, -1.0), new ComplexNumberType(-5.0, 2.0), new ComplexNumberType(-5.0, 2.0)).ToString());
Console.WriteLine(ComplexDo.ComplexAbs(new ComplexNumberType(0.5, Math.Sqrt(3) / 2)));
Console.WriteLine(ComplexDo.ComplexArg(new ComplexNumberType(0.5, Math.Sqrt(3) / 2)));
double abs = ComplexDo.ComplexAbs(new ComplexNumberType(1.0, Math.Sqrt(3.0)));
double arg = ComplexDo.ComplexArg(new ComplexNumberType(1.0, Math.Sqrt(3.0)));
Console.WriteLine($"{abs}      {arg}");

Console.WriteLine(ComplexDo.ComplexPowTrigForm(new ComplexNumberType(2.0, 5.0), 3).ToString());

ComplexDo.GetNComplexRoots(new ComplexNumberType(2.0, 5.0), 5);
Console.WriteLine((new ComplexNumberType(3.0,2.0),new ComplexNumberType(3.0, 2.0)));

var a = new ComplexNumberType(3.0, 5.0);
object d = new ComplexNumberType(5.2, 3.6);
Console.WriteLine(a.Equals(new ComplexNumberType(3.0, 6.0)));
Console.WriteLine(d.Equals(d));
Console.WriteLine(a.GetHashCode());
Console.WriteLine(new ComplexNumberType(default, default));

