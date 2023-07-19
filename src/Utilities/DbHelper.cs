using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.Utilities
{
    // describes database structure
    public class DbHelper
    {
        public static List<string> tables = new List<string> { "Movies", "Alerts" };
        public static string[][] columns = new string[2][] {
            new string[] { "Id", "Title", "Genre", "Premiered" },
            new string[] { "Id", "AlertLevel", "Title", "Message", "TimeCreated", "TimeIssued"}
        };


    }
}