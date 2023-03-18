namespace Autocomplete.Async
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Serilog;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private static CancellationTokenSource _token;

        public async Task<string> FindBestSimilarAsync(string example)
        {
            if (_token != null)
            {
                _token.Cancel();
            }

            _token = new CancellationTokenSource();
            CancellationToken token = _token.Token;

            var wordResult = BestSimilarInArray(SimpleWords, example, token);
            var movieResult = BestSimilarInArray(MovieTitles, example, token);
            var stageResult = BestSimilarInArray(StageNames, example, token);

            var word = await wordResult;
            var movie = await movieResult;
            var stage = await stageResult;
            if (word.SimilarityScore > movie.SimilarityScore &&
                word.SimilarityScore > stage.SimilarityScore)
            {
                return word.Line;
            }

            if (movie.IsBetterThan(stage))
            {
                return movie.Line;
            }

            return stage.Line;
        }

        public async void HandleTyping(HintedControl control)
        {
            control.Hint = await FindBestSimilarAsync(control.LastWord);
        }

        internal static async Task<SimilarLine> BestSimilarInArray(string[] lines, string example, CancellationToken token)
        {
            var best = new SimilarLine(string.Empty, 0);

            var task = Task.Factory.StartNew<SimilarLine>(() =>
            {
                foreach (var line in lines)
                {
                    if (token.IsCancellationRequested)
                    {
                        return new SimilarLine(string.Empty, 0);
                    }

                    var currentLine = new SimilarLine(line, line.Similarity(example));
                    if (currentLine.IsBetterThan(best))
                    {
                        best = currentLine;
                    }
                }

                return best;
            });
            return await task;
        }
    }
}
