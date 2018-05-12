using System;
using System.Linq;

namespace ElementsLib.Elements.Config.Results
{
    using TSeconds = Int32;
    using ResultCode = UInt16;

    using Module.Items.Utils;
    using Module.Libs;

    public class WeekScheduleValue : GeneralResultValue
    {
        public WeekScheduleValue(ResultCode code, TSeconds[] hoursWeek) : base(code)
        {
            HoursWeek = hoursWeek.ToArray();
        }
        public TSeconds[] HoursWeek { get; protected set; }
        public override string Description()
        {
            string formatedValue = string.Join("; ", HoursWeek.Select((h) => (FormatUtils.HoursFormat(h))));
            return string.Format("{0}: Hours in week: {1}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                formatedValue);
        }
        public override string ToResultExport(string targetSymbol)
        {
            TSeconds hoursSummary = HoursWeek.Aggregate(0, (agr, x) => (agr + x));

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
