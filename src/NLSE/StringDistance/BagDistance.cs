using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.NLSE.StringDistance
{
    public class BagDistance : IStringComparator
    {
        public static int GetDistance(string a, string b)
        {
            if (a == b)
                return 0;

            var bag_a = a.ToCharArray();
            var bag_b = b.ToCharArray();

            var bag_a_filtered = a.ToCharArray().ToList();
            var bag_b_filtered = b.ToCharArray().ToList();

            for (int i = 0; i < bag_a.Length; i++)
            {
                if (bag_b_filtered.Contains(bag_a[i]))
                {
                    bag_a_filtered.Remove(bag_a[i]);
                    bag_b_filtered.Remove(bag_a[i]);
                }
            }

            var bag_b_filtered_copy = bag_b_filtered.ToArray();

            for (int i = 0; i < bag_b_filtered_copy.Length; i++)
            {
                if (bag_a_filtered.Contains(bag_b_filtered_copy[i]))
                {
                    bag_b_filtered.Remove(bag_b_filtered_copy[i]);
                    bag_a_filtered.Remove(bag_b_filtered_copy[i]);
                }
            }

            return Math.Max(bag_a_filtered.Count, bag_b_filtered.Count);
        }

        public static float GetDistanceNormalized(string a, string b)
        {
            float dl = GetDistance(a, b);

            var normalized = 1f - (float)(dl / Math.Max(a.Length, b.Length));
            return normalized;
        }
    }
}