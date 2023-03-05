using System;
using System.Collections.Generic;
using System.Text;

namespace BusanBimsLib.Utilities
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string s)
        {
            return s[0].ToString().ToLowerInvariant() + s[1..];
        }
    }
}
