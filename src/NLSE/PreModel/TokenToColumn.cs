using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using NLSearchWeb.src.Utilities;

namespace NLSearchWeb.src.NLSE.PreModel
{
    public class TokenToColumn : TokenTo
    {
        public string _tableName { get; set; }
        public string _columnName { get; set; }
        public float _similarity = -1;


        public TokenToColumn(string token, string columnName, string tableName = null, float similarity = -1) : base(token)
        {
            _columnName = columnName;
            _tableName = tableName;
            _similarity = similarity;
        }
    }
}