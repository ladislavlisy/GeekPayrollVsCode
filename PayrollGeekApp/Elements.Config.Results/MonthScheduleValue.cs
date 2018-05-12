using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Results
{
    using TSeconds = Int32;
    using ResultCode = UInt16;

    using Module.Items.Utils;
    using Module.Libs;

    public class MonthScheduleValue : GeneralResultValue
    {
        public MonthScheduleValue(ResultCode code, TSeconds[] hoursMonth) : base(code)
        {
            HoursMonth = hoursMonth.ToArray();
        }
        public TSeconds[] HoursMonth { get; protected set; }
        public override string Description()
        {
            TSeconds hoursSummary = HoursMonth.Aggregate(0, (agr, x) => (agr + x));
            string formatedMonth = string.Join("; ", HoursMonth.Take(7).Select((h) => (FormatUtils.HoursFormat(h))));
            string formatedValue = FormatUtils.HoursFormat(hoursSummary);
            return string.Format("{0}: Hours in month: {1}; First Week: {2}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                formatedValue, formatedMonth);
        }
        public override string ToResultExport(string targetSymbol)
        {
            TSeconds hoursSummary = HoursMonth.Aggregate(0, (agr, x) => (agr + x));

            string hoursFormated = FormatUtils.HoursFormat(hoursSummary);
            string dayesFormated = "";
            string moneyFormated = "";
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}
