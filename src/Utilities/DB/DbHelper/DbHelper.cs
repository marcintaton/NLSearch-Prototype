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

    }
}