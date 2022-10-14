namespace QuadraticEqRoot
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please set the quadratic equation");

            Console.Write("a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.Write("c: ");
            double c = Convert.ToDouble(Console.ReadLine());

            double x = a + b + Math.Sqrt(c);
            if (x > 0)
            {
                x = x - 10;
            }

            Console.WriteLine("The root is {0}", x);
        }
    }
}
