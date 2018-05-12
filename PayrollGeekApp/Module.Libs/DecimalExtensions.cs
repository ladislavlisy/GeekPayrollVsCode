using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ElementsLib.Module.Libs
{
    public static class DecimalExtensions
    {
        public static string FormatAmount(this Decimal value)
        {
            string stringValue = value.ToString();

            return Regex.Replace(stringValue, "(\\d)(?=(\\d\\d\\d)+(?!\\d))", p => p.Groups[1].Value + " ",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
}
