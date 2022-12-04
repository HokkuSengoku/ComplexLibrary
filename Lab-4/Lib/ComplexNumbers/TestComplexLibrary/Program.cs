using System.Security.Cryptography;
using ComplexActions;

// Complex numbers
var a = new ComplexNumberType(1.0, 2.0);
var b = new ComplexNumberType(2.0, 5.0);
var c = new ComplexNumberType(15.0, 82.3);
int n = 3;
Console.WriteLine($"Complex number a: {a.ToString()}\n" +
                  $"Complex number b: {b.ToString()}\n" +
                  $"Complex number c: {c.ToString()}\n" +
                  $"Degree indicator n: {n}\n");

// The sum of two complex numbers
Console.WriteLine($"Sum of two complex numbers a and b: " +
                  $"{ComplexDo.ComplexSum(a, b)}\n");

// The sum of the set of complex numbers
Console.WriteLine($"The sum of the set of complex numbers a, b, c: " +
                  $"{ComplexDo.ComplexSum(a, b, c)}\n");

// The difference of two complex numbers.
Console.WriteLine($"The difference of two complex numbers a, b: " +
                  $"{ComplexDo.ComplexSubstract(a, b)}\n");

// Quotient of two complex numbers.
Console.WriteLine($"The quotient of two complex numbers a, c: " +
                  $"{ComplexDo.ComplexDiv(a, c)}\n");

// The product of two complex numbers.
Console.WriteLine($"The product of two complex numbers b, c: " +
                  $"{ComplexDo.ComplexMultiplication(b, c)}\n");

// The product of a set of complex numbers.
Console.WriteLine($"The product of a set of complex numbers a, b, c: " +
                  $"{ComplexDo.ComplexMultiplication(a, b, c)}\n");

// The argument of a complex number b.
Console.WriteLine($"The argument of a complex number b: {ComplexDo.ComplexArg(b)}\n");

// The module of a complex number b.
Console.WriteLine($"The module of a complex number b: {ComplexDo.ComplexAbs(b)}\n");

// A complex number b to the power of N
Console.WriteLine($"A complex number b to the power of N: {ComplexDo.ComplexPowTrigForm(b, n)}\n");

// The root of the 3rd degree of the complex number b.
Console.WriteLine($"The root of the 3rd degree of the complex number b: ");
ComplexDo.GetNComplexRoots(b, n);
Console.WriteLine();

// Output of a complex number B as a string
Console.WriteLine($"Output of a complex number B as a string: {b.ToString()}\n");

// The Equals method for complex numbers.
object d = new ComplexNumberType(5.2, 3.6);
Console.WriteLine($"Checking via equals of the d object. Result: {d.Equals(d)}");

// Comparison of two instances of complex number objects.
Console.WriteLine($"Comparison of two instances of complex number objects A and D. Result: {a.Equals(d)}");

// Getting the hash code of a complex number.
Console.WriteLine($"Getting the hash code of a complex number A. Result: {a.GetHashCode()}");
