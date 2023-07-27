using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLSearchWeb.src.NLSE.Model;

namespace NLSearchWeb.src.NLSE
{
    public class QueryBuilder
    {
        public static string Build(Model.Model model)
        {
            if (model.binds.Count == 0) return "";

            var query = "";

            var tableGroup = model.binds.GroupBy(x => x.table);

            foreach (var table in tableGroup)
            {
                var columnGroup = table.GroupBy(x => x.column);

                for (int i = 0; i < columnGroup.Count(); i++)
                {
                    var column = columnGroup.ElementAt(i);

                    var columnQuery = "(";

                    for (int j = 0; j < column.Count(); j++)
                    {
                        var value = column.ElementAt(j);
                        columnQuery += $" {value.column} = '{value.value}'";
                        if (j != column.Count() - 1) columnQuery += " OR";
                    }

                    columnQuery += ") ";

                    if (i != columnGroup.Count() - 1) columnQuery += "AND ";

                    query += columnQuery;
                }
            }

            // var sqlQuery = "select * from " + b.table + " where " + b.column + " = " + b.value;

            var tables = tableGroup.Select(x => x.Key);

            return "select * from " + string.Join(", ", tables) + " where " + query;
        }
    }
}