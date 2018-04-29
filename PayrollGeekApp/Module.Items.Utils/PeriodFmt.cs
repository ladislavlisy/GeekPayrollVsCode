using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Items.Utils
{
    public static class PeriodFmt
    {
        const string PERIOD_FMT_TEMPLATE = "MMMM yyyy";
        const string PERIOD_FMT_COUNTRY = "en-US";
        public static string Description(Period period)
        {
            CultureInfo enCultureInfo = new CultureInfo(PERIOD_FMT_COUNTRY);

            DateTime firstPeriodDay = period.BeginOfMonth();

            return firstPeriodDay.ToString(PERIOD_FMT_TEMPLATE, enCultureInfo);
        }

    }
}
