namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var letterDictionary = new Dictionary<string, string>();

            var oldWinEncoding = Encoding.GetEncoding(1251);
            var winBytes = File.ReadAllBytes(@"Data\Начало.txt");
            var file2Text = oldWinEncoding.GetString(winBytes);

            string file1Path = @"Data\Беда_одна_не_ходит.txt";
            string file3Path = @"Data\Хэппи_Энд.txt";

            int wordCount = 0;

            wordCount += CalculateWordCount(ReadFile(file1Path), letterDictionary);
            wordCount += CalculateWordCount(file2Text, letterDictionary);
            wordCount += CalculateWordCount(ReadFile(file3Path), letterDictionary);

            Console.WriteLine("Count of words in story files: {0}", wordCount);
        }

        private static int CalculateWordCount(string text, Dictionary<string, string> letterDictionary)
        {
            int wordCount = 0;
            string result;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            var textSplit = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();

            foreach (var letter in textSplit)
            {
                for (var i = 0; i < letter.Length; i++)
                {
                    if (char.IsLetter(letter[i]))
                    {
                        sb.Append(char.ToLower(letter[i]));
                    }
                }

                result = sb.ToString();

                if (!letterDictionary.ContainsKey(result) && result != string.Empty)
                {
                    letterDictionary[result] = result;
                    wordCount++;
                }

                sb.Clear();
            }

            return wordCount;
        }

        private static string ReadFile(string path)
        {
            var sb = new StringBuilder();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return sb.ToString();
        }
    }
}
