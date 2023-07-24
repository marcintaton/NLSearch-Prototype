using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MessagePack.Resolvers;
using NLSearchWeb.src.NLSE.PreModel;

namespace NLSearchWeb.src.Utilities.DB
{
    // describes database structure
    public class DbHelper
    {
        public static DBMock DBStructure { get; set; }

        // public static List<string> tables = new List<string> { "Movies", "Alerts" };
        // public static string[][] columns = new string[2][] {
        //     new string[] { "Id", "Title", "Genre", "Premiered" },
        //     new string[] { "Id", "AlertLevel", "Title", "Message", "TimeCreated", "TimeIssued"}
        // };

        public static async Task Init()
        {
            var dbData = await File.ReadAllTextAsync("src/Data/DBSeed.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var data = JsonSerializer.Deserialize<DBMock>(dbData);

            DBStructure = data;
        }

        public static TokenToTable FindTable(string token, string language)
        {
            var results = new List<TokenToTable>();

            foreach (var table in DBStructure.Tables)
            {
                foreach (var word in table.Title.Pools.Find(p => p.Language == language).Pool)
                {
                    var score = StringCompare.Compare(token, word);

                    results.Add(new TokenToTable(token, table.Title.Origin, score));
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

                if (results[0]._similarity >= 0.7)
                    return results[0];
            }

            return null;
        }

        public static TokenToColumn FindColumn(string token, string language)
        {
            var results = new List<TokenToColumn>();

            foreach (var table in DBStructure.Tables)
            {
                foreach (var column in table.Columns)
                {
                    foreach (var word in column.Pools.Find(p => p.Language == language).Pool)
                    {
                        var score = StringCompare.Compare(token, word);

                        results.Add(new TokenToColumn(token, column.Origin, table.Title.Origin, score));
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

                if (results[0]._similarity >= 0.7)
                    return results[0];
            }

            return null;
        }
    }
}