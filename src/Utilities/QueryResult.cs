using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLSearchWeb.src.NLSE;

namespace NLSearchWeb.src.Utilities
{
    public class QueryResult
    {
        public TokenTranslation[] Translations { get; set; }
        public string[] sqlQueries { get; set; }
    }
}