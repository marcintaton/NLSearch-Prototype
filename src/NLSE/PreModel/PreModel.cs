using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLSearchWeb.src.NLSE.PreModel;

namespace NLSearchWeb.src.NLSE
{
    public class PreModelDef
    {
        public List<TokenToTable> tables { get; private set; } = new();
        public List<TokenToColumn> columns { get; private set; } = new();
        public List<TokenToValue> values { get; private set; } = new();

        public void Sort()
        {
            tables.Sort((a, b) => a._dlDistance - b._dlDistance);
            columns.Sort((a, b) => a._dlDistance - b._dlDistance);
        }
    }
}