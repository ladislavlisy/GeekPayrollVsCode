using System;

namespace ElementsLib.Elements.Config.Results
{
    using ResultCode = UInt16;

    using ElementsLib.Legalist.Constants;
    using Module.Libs;
    public class TermFromStopPositionValue : GeneralResultValue
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public WorkPositionType PositionType { get; set; }

        public TermFromStopPositionValue(DateTime? dateFrom, DateTime? dateStop, WorkPositionType positionType) : base((ResultCode)ArticleResultCode.RESULT_VALUE_FROM_STOP_POSITION)
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
    }
}
