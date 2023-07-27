using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MessagePack.Resolvers;
using NLSearchWeb.src.NLSE.PreModel;
using NLSearchWeb.src.NLSE.StringDistance;

namespace NLSearchWeb.src.Utilities.DB
{
    // describes database structure
    public partial class DbHelper
    {
        public static List<TokenToTable> CompareTables(string token, string language)
        {
            var results = new List<TokenToTable>();

            foreach (var table in DBStructure.Tables)
            {
                foreach (var word in table.Title.Pools.Find(p => p.Language == language).Pool)
                {
                    var score = StringCompare.Compare(token, word);

                    results.Add(new TokenToTable(token, table.ToString(), score));
                }
            }

            if (results.Count > 0)
            {
                results.Sort((a, b) => (int)((b._similarity - a._similarity) * 1000));

                // Console.WriteLine();
                // Console.WriteLine("Tables " + token);
                // foreach (var c in results)
                // {
                //     Console.WriteLine(c._similarity);
                // }
            }

            return results;
        }

        public static List<TokenToColumn> CompareColumns(string token, string language)
        {
            var results = new List<TokenToColumn>();

            foreach (var table in DBStructure.Tables)
            {
                foreach (var column in table.Columns)
                {
                    foreach (var word in column.Title.Pools.Find(p => p.Language == language).Pool)
                    {
                        var score = StringCompare.Compare(token, word);

                        results.Add(new TokenToColumn(token, column.ToString(), table.ToString(), score));
                    }
                }
            }

            if (results.Count > 0)
            {
                results.Sort((a, b) => (int)((b._similarity - a._similarity) * 1000));

                // Console.WriteLine();
                // Console.WriteLine("Columns " + token);
                // foreach (var c in results)
                // {
                //     Console.WriteLine(c._similarity);
                // }
            }

            return results;
        }
    }
}