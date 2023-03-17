namespace Autocomplete.Basic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        public static string FindBestSimilar(SimilarLine stageResult, SimilarLine movieResult, SimilarLine wordResult)
        {
            if (wordResult.SimilarityScore > movieResult.SimilarityScore &&
                wordResult.SimilarityScore > stageResult.SimilarityScore)
            {
                return wordResult.Line;
            }

            return (stageResult.IsBetterThan(movieResult) ? stageResult : movieResult).Line;
        }

        public void HandleTyping(HintedControl control)
        {
            Task<SimilarLine> task1 = Task.Run(() =>
            {
                return BestSimilarInArray(SimpleWords, control.LastWord);
            });
            Task<SimilarLine> task2 = Task.Run(() =>
            {
               return BestSimilarInArray(MovieTitles, control.LastWord);
            });
            Task<SimilarLine> task3 = Task.Run(() =>
            {
                return BestSimilarInArray(StageNames, control.LastWord);
            });
            Task.WaitAll(task1, task2, task3);

            control.Hint = FindBestSimilar(task1.Result, task2.Result, task3.Result);
        }

        internal static SimilarLine BestSimilarInArray(string[] lines, string example)
        {
            return lines.Aggregate(
                new SimilarLine(string.Empty, 0),
                (SimilarLine best, string line) =>
                {
                    var current = new SimilarLine(line, line.Similarity(example));

                    return current.IsBetterThan(best) ? current : best;
                });
        }
    }
}
