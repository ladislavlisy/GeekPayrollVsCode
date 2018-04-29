using System;

namespace ElementsLib.Elements.Config.Results
{
    using ResultCode = UInt16;

    using Module.Interfaces.Elements;
    public abstract class GeneralResultValue : IArticleResultValues
    {
        public GeneralResultValue(ResultCode code)
        {
            Code = code;
        }
        protected ResultCode Code { get; set; }
        public abstract string Description();
        public bool IsResultCodeValue(ResultCode code)
        {
            return (this.Code == code);
        }

        public bool IsContractFromStopValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_FROM_STOP_CONTRACT);
        }
        public bool IsPositionFromStopValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_FROM_STOP_POSITION);
        }
        public bool IsFullWeeksValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_FULL_WEEKS_HOURS);
        }
        public bool IsPartWeeksValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_REAL_WEEKS_HOURS);
        }
        public bool IsMonthFromStopValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_MONTH_FROM_STOP);
        }
        public bool IsFullMonthValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_FULL_MONTH_HOURS);
        }
        public bool IsRealMonthValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_REAL_MONTH_HOURS);
        }
        public bool IsTermMonthValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_TERM_MONTH_HOURS);
        }
    }
}