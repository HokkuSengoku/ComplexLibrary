namespace Autocomplete.Basic
{
    using System;

    public static class StringMetrics
    {
        /// <summary>
        /// The implementation of the "naive algoritm" solving classical problem
        /// http://en.wikipedia.org/wiki/Longest_common_subsequence_problem
        /// The only improvement was made in that it uses memory for one current row
        /// instead of the whole matrix LCS.
        /// </summary>
        /// <param name="str1">First string.</param>
        /// <param name="str2">Second string.</param>
        /// <returns>An amount of common characters.</returns>
        public static int LongestCommonSubstringLength(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            var len1 = str1.Length;
            var len2 = str2.Length;

            if (len2 > len1)
            {
                var tempStr = str1;
                str1 = str2;
                str2 = tempStr;

                var tempLen = len1;
                len1 = len2;
                len2 = tempLen;
            }

            var lcsLastRow = new int[len2 + 1];
            for (var i = 0; i < len1; i++)
            {
                var c00 = 0;
                for (var j = 0; j < len2; j++)
                {
                    var c01 = lcsLastRow[j + 1];
                    lcsLastRow[j + 1] = (str1[i] == str2[j]) ? c00 + 1 : Math.Max(c01, lcsLastRow[j]);
                    c00 = c01;
                }
            }

            return lcsLastRow[len2];
        }

        public static int Similarity(this string str1, string str2)
        {
            return LongestCommonSubstringLength((str1 ?? string.Empty).ToLower(), (str2 ?? string.Empty).ToLower());
        }
    }
}
