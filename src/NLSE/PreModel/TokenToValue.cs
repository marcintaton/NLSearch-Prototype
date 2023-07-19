using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.NLSE.PreModel
{
    public class TokenToValue : TokenTo
    {
        public string _value { get; set; }

        public TokenToValue(string token, string value) : base(token)
        {
            _value = value;
        }
    }
}