using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLSearchWeb.src.Utilities;

namespace NLSearchWeb.src.NLSE.PreModel
{
    public class TokenToTable : TokenTo
    {
        public string _tableName { get; set; }
        public float _similarity = -1;

        public TokenToTable(string token, string tableName, float similarity = -1) : base(token)
        {
            _tableName = tableName;
            _similarity = similarity;
        }
    }
}