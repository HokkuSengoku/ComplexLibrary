namespace QuadraticEqRoot2
{
    using System.Collections.Generic;
    using System.IO;

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
                    double a = 1; // todo: get from line
                    double b = 2; // todo: get from line
                    double c = 3; // todo: get from line

                    IList<double> roots = Solver.Solve(a, b, c);

                    string outputLine = "todo";

                    output.WriteLine(outputLine);
                }
            }
        }
    }
}
