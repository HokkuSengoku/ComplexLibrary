namespace ColorReplacement
{
    using System;
    using System.IO;
    using System.Text;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var source = new StreamReader("Data/colors.txt", Encoding.UTF8))
            {
                // initializes colors
            }

            using (var source = new StreamReader("Data/source.txt", Encoding.UTF8))
            using (var target = new StreamWriter("Data/target.txt"))
            {
                // reads source.txt, replaces colors, writes target.txt, collects data about replaced colors
            }

            using (var target = new StreamWriter("Data/used_colors.txt"))
            {
                // writes data about replaced colors
            }
        }
    }
}
