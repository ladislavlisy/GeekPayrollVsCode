using System;

namespace ElementsLib.Elements.Config.Results
{
    using ResultCode = UInt16;

    using ElementsLib.Legalist.Constants;
    using Module.Libs;
    public class TermFromStopContractValue : GeneralResultValue
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public WorkEmployTerms ContractType { get; set; }

        public TermFromStopContractValue(DateTime? dateFrom, DateTime? dateStop, WorkEmployTerms contractType) : base((ResultCode)ArticleResultCode.RESULT_VALUE_FROM_STOP_POSITION)
        {
            DateFrom = dateFrom;
            DateStop = dateStop;
            ContractType = contractType;
        }
        public override string Description()
        {
            return string.Format("{0}: Date FROM: {1}, Date STOP: {2}, Position: {3}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                DateFrom.Format(), DateStop.Format(), ContractType.GetSymbol());
        }
    }
}
