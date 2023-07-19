using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.Utilities
{
    public class DamerauLevenshtein
    {
        public static int GetDistance(string a, string b)
        {

            if (a == b)
                return 0;

            int len_a = a.Length;
            int len_b = b.Length;

            if (len_a == 0 || len_b == 0)
                return len_a == 0 ? len_b : len_a;

            var matrix = new int[len_a + 1, len_b + 1];

            for (int i = 1; i <= len_a; i++)
            {
                matrix[i, 0] = i;
                for (int j = 1; j <= len_b; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    if (i == 1)
                        matrix[0, j] = j;

                    var vals = new int[] {
                    matrix[i - 1, j] + 1,
                    matrix[i, j - 1] + 1,
                    matrix[i - 1, j - 1] + cost
                };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_a, len_b];
        }
    }
}