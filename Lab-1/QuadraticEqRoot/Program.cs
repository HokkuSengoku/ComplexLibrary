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

            double d = Math.Pow(b,2) - 4 *(a*c);
            if (d > 0)
                Console.WriteLine($"x1 = {(-b + Math.Sqrt(d)) / (2 * a)}"+
             $" x2 = {(-b - Math.Sqrt(d)) / (2 * a)}");
            else if (d == 0)
                Console.WriteLine($"x = {-b / (2 * a)}");
            else
                Console.WriteLine("No roots");
        }
    }
}
