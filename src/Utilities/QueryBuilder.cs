using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLSearchWeb.src.NLSE.Model;

namespace NLSearchWeb.src.Utilities
{
    public class QueryBuilder
    {
        public static string Build(Model model)
        {
            if (model.binds.Count == 0) return "";

            var b = model.binds[0];

            var sqlQuery = "select * from " + b.table + " where " + b.column + " = " + b.value;

            return sqlQuery;
        }
    }
}