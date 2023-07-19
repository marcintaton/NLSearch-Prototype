using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace NLSearchWeb.src.NLSE.PreModel
{
    public abstract class TokenTo
    {
        public string _token;

        public TokenTo(string token)
        {
            _token = token;
        }
    }
}