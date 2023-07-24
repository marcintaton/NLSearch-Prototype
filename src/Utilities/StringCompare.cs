using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NLSearchWeb.src.Utilities
{
    public class StringCompare
    {
        // returns normalized distance using current strategy
        public static float Compare(string a, string b)
        {
            return NormalizedDamerauLevenshteinDistance(a, b);
        }

        private static float NormalizedDamerauLevenshteinDistance(string a, string b)
        {
            float dl = DamerauLevenshtein.GetDistance(a, b);

            var normalized = 1f - (float)(dl / Math.Max(a.Length, b.Length));
            // Console.WriteLine("norm: " + a + " " + b + " " + normalized);
            return normalized;
        }

        // additional algorithms go here
    }
}