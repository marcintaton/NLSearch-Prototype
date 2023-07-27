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
        public static List<DbReference> GetColumnsForValue(string value)
        {
            var tag = ValueTypeHelper.GetValueType(value);

            var response = new List<DbReference>();

            foreach (var table in DBStructure.Tables)
            {
                foreach (var column in table.Columns)
                {
                    if (column.ValueTags.Contains(tag.ToString()))
                    {
                        response.Add(new DbReference { columnName = column.ToString(), tableName = table.ToString() });
                    }
                }
            }

            return response;
        }
    }
}