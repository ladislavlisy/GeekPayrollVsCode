using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class DateTimeExtensions
    {
        const string PERIOD_FMT_TEMPLATE = "MMMM yyyy";
        const string PERIOD_FMT_COUNTRY = "en-US";
        public static string Format(this DateTime? value)
        {
            if (value.HasValue)
            {
                CultureInfo enCultureInfo = new CultureInfo(PERIOD_FMT_COUNTRY);

                return value.Value.ToString(PERIOD_FMT_TEMPLATE, enCultureInfo);
            }
            return "NONE";
        }
    }
}
