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

            var y = Function((b - a) / 2);

            Console.WriteLine("Корень уравнения равен " + y);
            Console.WriteLine("Количество итераций равно " + 1);
        }
    }
}
