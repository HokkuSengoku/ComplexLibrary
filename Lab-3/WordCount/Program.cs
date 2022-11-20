namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Program
    {
        private static HashSet<string> uniqueWords = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var oldWinEncoding = Encoding.GetEncoding(1251);
            var winBytes = File.ReadAllBytes(@"Data\Начало.txt");
            var file2Text = oldWinEncoding.GetString(winBytes);

            string file1Path = @"Data\Беда_одна_не_ходит.txt";
            string file3Path = @"Data\Хэппи_Энд.txt";

            ReadFile(file1Path);
            CalculateWordCount(file2Text);
            ReadFile(file3Path);
            Console.WriteLine("Count of words in story files: {0}", uniqueWords.Count);
        }

        private static void CalculateWordCount(string text)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-', '!', '?' };
            var textSplit = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var word in textSplit)
            {
                uniqueWords.Add(word);
            }
        }

        private static void ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        CalculateWordCount(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
