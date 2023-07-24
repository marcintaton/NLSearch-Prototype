using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.Utilities.DB
{
    public class WordPool
    {
        public string Origin { get; set; }
        public List<LangPool> Pools { get; set; }
    }
}