using System;

namespace ElementsLib.Elements.Config.Results
{
    using TDay = Byte;
    using ResultCode = UInt16;

    using Module.Libs;

    public class MonthFromStopValue : GeneralResultValue
    {
        public TDay PeriodDayFrom { get; protected set; }
        public TDay PeriodDayStop { get; protected set; }

        public MonthFromStopValue(byte dayFrom, byte dayStop) : base((ResultCode)ArticleResultCode.RESULT_VALUE_MONTH_FROM_STOP)
        {
            this.PeriodDayFrom = dayFrom;
            this.PeriodDayStop = dayStop;
        }
        public override string Description()
        {
            return string.Format("{0}: Day FROM: {1}, Day STOP: {2}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(), 
                PeriodDayFrom.ToString(), PeriodDayStop.ToString());
        }
        public override string ToResultExport(string targetSymbol)
        {
            Int32 dayesSummary = (PeriodDayStop - PeriodDayFrom + 1);

            string hoursFormated = "";
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