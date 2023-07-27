using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;

namespace NLSearchWeb.src.NLSE.StringDistance
{
    public class FuzzyMatch : IStringComparator
    {
        public static int GetDistance(string a, string b)
        {
            return Fuzz.Ratio(a, b);
        }

        public static float GetDistanceNormalized(string a, string b)
        {
            var result = GetDistance(a, b);

            return (float)(result / 100f);
        }
    }
}