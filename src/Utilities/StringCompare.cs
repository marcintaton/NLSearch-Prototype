using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;
using HtmlAgilityPack;

namespace NLSearchWeb.src.Utilities
{
    public class StringCompare
    {
        // returns normalized similarity using current strategy
        // closer to 1.0 means more similar
        public static float Compare(string a, string b)
        {
            // return NormalizedDamerauLevenshteinDistance(a, b);
            return NormalizedFuzzyMatching(a, b);
        }

        private static float NormalizedDamerauLevenshteinDistance(string a, string b)
        {
            float dl = DamerauLevenshtein.GetDistance(a, b);

            var normalized = 1f - (float)(dl / Math.Max(a.Length, b.Length));
            return normalized;
        }

        // additional algorithms go here
        private static float NormalizedFuzzyMatching(string a, string b)
        {
            // returns 100 for perfect match
            var result = Fuzz.Ratio(a, b);

            return (float)(result / 100f);
        }
    }
}