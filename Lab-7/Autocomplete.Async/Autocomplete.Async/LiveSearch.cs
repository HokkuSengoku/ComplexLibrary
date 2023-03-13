namespace Autocomplete.Async
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private CancellationTokenSource _token;

        public async Task<string> FindBestSimilarAsync(string example)
        {
            if (_token != null)
            {
                _token.Cancel();
            }

            _token = new CancellationTokenSource();

            var task = Task.Factory.StartNew<string>(
                (o) =>
                {
                    var ct = (CancellationTokenSource)o;

                    var stageResult = BestSimilarInArray(StageNames, example);
                    if (ct.IsCancellationRequested)
                    {
                        return string.Empty;
                    }

                    var movieResult = BestSimilarInArray(MovieTitles, example);

                    if (ct.IsCancellationRequested)
                    {
                        return string.Empty;
                    }

                    var wordResult = BestSimilarInArray(SimpleWords, example);

                    if (ct.IsCancellationRequested)
                    {
                        return string.Empty;
                    }

                    if (wordResult.SimilarityScore > movieResult.SimilarityScore && wordResult.SimilarityScore > stageResult.SimilarityScore)
                    {
                        return wordResult.Line;
                    }

                    if (movieResult.IsBetterThan(stageResult))
                    {
                        return movieResult.Line;
                    }

                    return stageResult.Line;
                }, _token);

            return await task;
        }

        public async void HandleTyping(HintedControl control)
        {
            control.Hint = await FindBestSimilarAsync(control.LastWord);
        }

        internal static SimilarLine BestSimilarInArray(string[] lines, string example)
        {
            var best = new SimilarLine(string.Empty, 0);

            foreach (var line in lines)
            {
                var currentLine = new SimilarLine(line, line.Similarity(example));

                if (currentLine.IsBetterThan(best))
                {
                    best = currentLine;
                }
            }

            return best;
        }
    }
}
