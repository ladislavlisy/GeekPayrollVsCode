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
        public abstract string ToResultExport(string targetSymbol);
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
        public bool IsRealWeeksValue()
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
        public bool IsMonthAttendanceValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_ATTN_MONTH_HOURS);
        }
        public bool IsPaymentMoneyValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_PAYMENT_MONEY);
        }
        public bool IsDeclarationTaxingValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_TAXING);
        }
        public bool IsDeclarationHealthValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_HEALTH);
        }
        public bool IsDeclarationSocialValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_SOCIAL);
        }
        public bool IsIncomeTaxableValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_INCOME_SUM_TAXING);
        }
        public bool IsTransferIncomeValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_TRANSFER_INCOME_MONEY);
        }
        public bool IsInsuranceBasisValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_INSURANCE_BASIS_MONEY);
        }
        public bool IsTaxingBasisValue()
        {
            return IsResultCodeValue((ResultCode)ArticleResultCode.RESULT_VALUE_TAXING_BASIS_MONEY);
        }
    }
}