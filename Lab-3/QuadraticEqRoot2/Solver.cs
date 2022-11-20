namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class Solver
    {
        public static IList<double> Solve(double a, double b, double c)
        {
            // todo: Replace with actual implementation
            List<double> roots = new List<double>();
            double epsilon = 0.000001;
            double d = Math.Pow(b, 2) - (4 * (a * c));
            if (Math.Abs(d) > epsilon)
            {
                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                roots.Add(x1);
                roots.Add(x2);

                roots.Sort();
                roots.Reverse();
            }

            if (Math.Abs(d) <= epsilon)
            {
                double x = -b / (2 * a);
                roots.Add(x);
            }

            return roots;
        }
    }
}