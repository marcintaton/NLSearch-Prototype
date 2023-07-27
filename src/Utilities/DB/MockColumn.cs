using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.Utilities.DB
{
    public class MockColumn
    {
        public WordPool Title { get; set; }
        public List<string> ValueTags { get; set; }

        public override string ToString()
        {
            return Title.Origin;
        }

    }
}