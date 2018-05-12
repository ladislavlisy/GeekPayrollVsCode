using System;

namespace ElementsLib.Elements.Config.Results
{
    using ResultCode = UInt16;

    using ElementsLib.Legalist.Constants;
    using Module.Libs;
    public class PositionFromStopValue : GeneralResultValue
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public WorkPositionType PositionType { get; set; }

        public PositionFromStopValue(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType) : base((ResultCode)ArticleResultCode.RESULT_VALUE_FROM_STOP_POSITION)
        {
            DateFrom = dateFrom;
            DateStop = dateStop;
            PositionType = positionType;
        }
        public override string Description()
        {
            return string.Format("{0}: Date FROM: {1}, Date STOP: {2}, Position: {3}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                DateFrom.Format(), DateStop.Format(), PositionType.GetSymbol());
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
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
