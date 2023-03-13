namespace Autocomplete.Basic
{
    public class SimilarLine
    {
        public SimilarLine(string line, int similarityScore)
        {
            Line = line;
            SimilarityScore = similarityScore;
        }

        /// <summary>
        /// Gets the original line text.
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// Gets line similarity score.
        /// </summary>
        public int SimilarityScore { get; }

        /// <summary>
        /// Checks if this line has better score than another one.
        /// </summary>
        public bool IsBetterThan(SimilarLine other)
        {
            return (SimilarityScore > other.SimilarityScore) || (SimilarityScore == other.SimilarityScore && Line.Length < other.Line.Length);
        }
    }
}