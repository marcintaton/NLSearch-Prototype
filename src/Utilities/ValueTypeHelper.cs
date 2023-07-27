using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NLSearchWeb.src.Utilities.DB;

namespace NLSearchWeb.src.Utilities
{
    public class ValueTypeHelper
    {
        // A simple mock algorithm to determine whether a value is a text, number or date,
        // Based on arbitrary assumptions
        public static DB.ValueType GetValueType(string origin)
        {
            var numCounter = 0;
            var letterCounter = 0;

            foreach (var c in origin.ToCharArray())
            {
                if (c >= '0' && c <= '9')
                    numCounter++;
                else
                    letterCounter++;
            }

            if (numCounter > letterCounter)
            {
                try
                {
                    var num = int.Parse(origin);

                    if (num >= 1900 && num <= 2050)
                    {
                        return DB.ValueType.Date;
                    }
                    else
                    {
                        return DB.ValueType.Number;
                    }
                }
                catch
                {
                    return DB.ValueType.Number;
                }
            }

            return DB.ValueType.Text;
        }
    }
}