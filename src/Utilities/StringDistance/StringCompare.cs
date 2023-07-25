using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;
using HtmlAgilityPack;

namespace NLSearchWeb.src.Utilities.StringDistance
{
    public class StringCompare
    {
        // returns normalized similarity using current strategy
        // closer to 1.0 means more similar
        public static float Compare(string a, string b)
        {
            // return DamerauLevenshtein.GetDistanceNormalized(a, b);
            // return FuzzyMatch.GetDistanceNormalized(a, b);
            return BagDistance.GetDistanceNormalized(a, b);
        }
    }
}