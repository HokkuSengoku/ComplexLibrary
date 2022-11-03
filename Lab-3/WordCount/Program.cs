namespace WordCount
{
    using System;
    using System.IO;
    using System.Text;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string file1Text = File.ReadAllText(@"Data\Беда_одна_не_ходит.txt");

            int wordCount = 0;

            wordCount += CalculateWordCount(file1Text);

            Console.WriteLine("Count of words in story files: {0}", wordCount);
        }

        private static int CalculateWordCount(string text)
        {
            int wordCount = 0;
            foreach (char ch in text)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    wordCount++;
                }
            }

            return wordCount;
        }
    }
}
