namespace Autocomplete.Basic
{
    using System.IO;
    using System.Linq;
    using System.Threading;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private Thread _searchThread;

        public static string FindBestSimilar(string example)
        {
            var stageResult = BestSimilarInArray(StageNames, example);
            var movieResult = BestSimilarInArray(MovieTitles, example);
            var wordResult = BestSimilarInArray(SimpleWords, example);

            if (wordResult.SimilarityScore > movieResult.SimilarityScore &&
                wordResult.SimilarityScore > stageResult.SimilarityScore)
            {
                return wordResult.Line;
            }

            return (stageResult.IsBetterThan(movieResult) ? stageResult : movieResult).Line;
        }

        public void HandleTyping(HintedControl control)
        {
            if (_searchThread != null)
            {
                _searchThread.Abort();
                _searchThread.Join();
            }

            _searchThread = new Thread(
                () =>
                {
                    control.Hint = FindBestSimilar(control.LastWord);
                });

            _searchThread.Start();
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
