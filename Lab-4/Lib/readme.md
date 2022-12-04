# A library for working with complex numbers.
## The library contains 1 class and 1 structure.
#### :exclamation: The structure ComplexNumberType consists of:
:one: A proprietary data type for working with complex numbers. **ComplexNumberType(double, double)**

:two: Operator overloads **'+', '-', '/', '*', '==', '!='** for working with complex numbers.

:three: Redefined **Equals(), ToString, GetHashCode()** methods for working with complex numbers.
#### :question: The ComplexDo class includes:
- **ComplexSum(ComplexNumberType x, ComplexNumberType y)** The sum of two complex numbers. 
The sum of two complex numbers. Returns an instance of **ComplexNumberType** that is the sum of two complex numbers.
- **ComplexSum(params ComplexNumberType[] values)** An analogue of the **ComplexSum()** method, only working with a set of parameters. 
Returns an instance of **ComplexNumberType** it is the sum of a set of complex numbers.
- **ComplexSubstract(ComplexNumberType x, ComplexNumberType y)** The difference of two complex numbers. Returns an instance of **ComplexNumberType**, which is the difference of two complex numbers.
- **ComplexMultiplication(ComplexNumberType x, ComplexNumberType y)** The product of two complex numbers. Returns an instance of **ComplexNumberType**, which is the product of two complex numbers.
- **ComplexMultiplication(params ComplexNumberType[] values)** An analogue of the **ComplexMultiplication()** method, only working with a set of parameters.
- **ComplexDiv(ComplexNumberType x, ComplexNumberType y)** A method for finding the quotient of two complex numbers. 
Returns an instance of **ComplexNumberType**, which is a quotient of the division of two complex numbers.
- **ComplexArg(ComplexNumberType x)** A method for finding the argument of a complex number. Returns an argument - a number of type **double**.
- **ComplexAbs(ComplexNumberType x)** A method for finding the modulus of a complex number. Returns an argument - a number of type **double**.
- **ComplexPowTrigForm(ComplexNumberType x, int powCounter)** A method for finding a complex number in the power of **N**. 
Returns an instance of **ComplexNumberType**, which is a complex number to the power of **N**.
- **GetNComplexRoots(ComplexNumberType x, int powCounter)** A method for finding roots of degree N from a complex number. Returns a list of the **ComplexNumberType** type containing all the roots of a complex number.

##### :white_check_mark: Thanks to this library, you can perform actions with complex numbers.
