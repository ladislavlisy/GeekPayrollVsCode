using System;

namespace ElementsLib.Elements.Config.Results
{
    using TDay = Byte;
    using TSeconds = Int32;
    using ResultCode = UInt16;

    using Module.Libs;
    using System.Linq;
    using Module.Items.Utils;

    public class MonthAttendanceValue : GeneralResultValue
    {
        public TDay PeriodDayFrom { get; protected set; }
        public TDay PeriodDayStop { get; protected set; }
        public TSeconds[] HoursMonth { get; protected set; }

        public MonthAttendanceValue(ResultCode code, TDay dayFrom, TDay dayStop, TSeconds[] hoursMonth) : base(code)
        {
            this.PeriodDayFrom = dayFrom;
            this.PeriodDayStop = dayStop;
            HoursMonth = hoursMonth.ToArray();
        }
        public override string Description()
        {
            TSeconds hoursSummary = HoursMonth.Aggregate(0, (agr, x) => (agr + x));
            string formatedValue = FormatUtils.HoursFormat(hoursSummary);
            return string.Format("{0}: Day FROM: {1}, Day STOP: {2}, Hours in month: {3}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                PeriodDayFrom.ToString(), PeriodDayStop.ToString(), formatedValue);
        }
        public override string ToResultExport(string targetSymbol)
        {
            TSeconds hoursSummary = HoursMonth.Aggregate(0, (agr, x) => (agr + x));
            Int32 dayesSummary = (PeriodDayStop - PeriodDayFrom + 1);

            string hoursFormated = FormatUtils.HoursFormat(hoursSummary);
            string dayesFormated = dayesSummary.ToString();
            string moneyFormated = "";
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}