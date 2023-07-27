using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.NLSE.StringDistance
{
    public interface IStringComparator
    {
        static abstract float GetDistanceNormalized(string a, string b);
        static abstract int GetDistance(string a, string b);
    }
}