namespace QuadraticEqRoot2
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            const string outputPath = "output.txt";

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (var input = File.OpenText("input.txt"))
            using (var output = File.CreateText(outputPath))
            {
                string line;

                while ((line = input.ReadLine()) != null)
                {
                    double[] coefficients = GetCoefficients(line);
                    double a = coefficients[0]; // todo: get from line
                    double b = coefficients[1]; // todo: get from line
                    double c = coefficients[2]; // todo: get from line

                    if (a == 0)
                    {
                        output.WriteLine("Error: a = 0, деление на 0 невозможно.");
                        continue;
                    }

                    IList<double> roots = Solver.Solve(a, b, c);
                    var myRootsFormated = roots.Select(d => d.ToString(CultureInfo.InvariantCulture));

                    string result = string.Join(",", myRootsFormated);
                    output.WriteLine(result);
                }
            }
        }

        private static double[] GetCoefficients(string line)
        {
            string[] coefString = line.Split(",");
            double[] coefficients = new double[3];

            double.TryParse(coefString[0], NumberStyles.Number, CultureInfo.InvariantCulture, out coefficients[0]);
            double.TryParse(coefString[1], NumberStyles.Number, CultureInfo.InvariantCulture, out coefficients[1]);
            double.TryParse(coefString[2], NumberStyles.Number, CultureInfo.InvariantCulture, out coefficients[2]);

            return coefficients;
        }
    }
}
