namespace ColorReplacement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static Dictionary<string, string> colorsList = new Dictionary<string, string>();
        private static SortedList<string, string> replacementColors = new SortedList<string, string>();

        private static void Main(string[] args)
        {
            Regex reg = new Regex(
                @"(rgb\(\d{1,3},\s*\d{1,3},\s*\d{1,3}\)|#[A-Fa-f0-9]{6}\b|#[A-Fa-f0-9]{3}\b)");
            string regPattern =
                @"(rgb\(\d{1,3},\s*\d{1,3},\s*\d{1,3}\)|#[A-Fa-f0-9]{6}\b|#[A-Fa-f0-9]{3}\b)";

            using (var source = new StreamReader("Data/colors.txt", Encoding.UTF8))
            {
                // initializes colors
                string line;

                while ((line = source.ReadLine()) != null)
                {
                    string key = line.Split(' ')[1];
                    string value = line.Split(' ')[0];
                    if (!colorsList.ContainsKey(key))
                    {
                        colorsList[key] = value;
                    }
                }
            }

            using (var source = new StreamReader("Data/source.txt", Encoding.UTF8))
            using (var target = new StreamWriter("Data/target.txt"))
            {
                string line;

                while ((line = source.ReadLine()) != null)
                {
                    line = Regex.Replace(line, regPattern, ReplaceDelegate);
                    target.WriteLine(line);
                }

                // reads source.txt, replaces colors, writes target.txt, collects data about replaced colors
            }

            const string outputPath = "Data/used_colors.txt";
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using (var target = new StreamWriter("Data/used_colors.txt", true))
            {
                foreach (var color in replacementColors)
                {
                    target.WriteLine($"{color.Key} {color.Value}");
                }
            }
        }

        private static string RgbConvert(string match)
        {
            char[] separators = new char[] { ',', '(', ')', 'r', 'g', 'b' };
            string[] numbers = match.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string resultHex = string.Empty;
            byte[] byteNumbers = new byte[3];

            for (var i = 0; i < numbers.Length; i++)
            {
                byteNumbers[i] = Convert.ToByte(numbers[i]);
            }

            resultHex = "#" + byteNumbers[0].ToString("X") + byteNumbers[1].ToString("X") +
                        byteNumbers[2].ToString("X");

            return resultHex;
        }

        private static string TripleHexConvert(string match)
        {
            string textReplace;
            var sb = new StringBuilder();
            sb.Append("#");

            for (var i = 1; i < match.Length; i++)
            {
                sb.Append(match[i]);
                sb.Append(match[i]);
            }

            textReplace = sb.ToString();

            return textReplace;
        }

        private static string ReplaceDelegate(Match match)
        {
            string replaceValueKey = string.Empty;

            if (match.Value.Length > 9)
            {
                replaceValueKey = RgbConvert(match.Value);
                replaceValueKey = ContainsColor(replaceValueKey, match.Value);

                return replaceValueKey;
            }

            if (match.Value.Length == 7)
            {
                replaceValueKey = match.Value;
                replaceValueKey = ContainsColor(replaceValueKey, match.Value);

                return replaceValueKey;
            }

            if (match.Value.Length == 4)
            {
                replaceValueKey = TripleHexConvert(match.Value);
                replaceValueKey = ContainsColor(replaceValueKey, match.Value);

                return replaceValueKey;
            }

            return replaceValueKey;
        }

        private static string ContainsColor(string replaceValueKey, string matchValue)
        {
            string replaceValue = string.Empty;

            if (colorsList.ContainsKey(replaceValueKey))
            {
                replaceValue = colorsList[replaceValueKey];
                if (!replacementColors.ContainsKey(replaceValueKey))
                {
                    replacementColors.Add(replaceValueKey, replaceValue);
                }

                return replaceValue;
            }
            else
            {
                return matchValue;
            }
        }
    }
}