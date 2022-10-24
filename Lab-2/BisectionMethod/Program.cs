namespace BisectionMethod
{
    using System;

    class Program
    {
        static double Function(double x)
        {
            return (x * x) - 2;
        }

        static void Main()
        {
            var a = 0.0;
            var b = 2.0;
            var eps = 0.0001;
            var halfAB = 0.0;
            var productInPoints = 0.0;
            var countOfIteration= 0;
            
            do
            {
                countOfIteration++;
                halfAB = (a + b) / 2;
                productInPoints = Function(halfAB) * Function(a);

                if (productInPoints > 0)
                    a = halfAB;
                else if (productInPoints < 0)
                    b = halfAB;
            } while (Math.Abs(a - b) > eps);
            
            Console.WriteLine("Корень уравнения равен " + halfAB);
            Console.WriteLine("Количество итераций равно " + countOfIteration);
        }
    }
}
