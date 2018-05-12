using System;

namespace ElementsLib.Elements.Config.Results
{
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using Legalist.Constants;

    public class IncomeInsHealthValue : GeneralResultValue
    {
        public WorkHealthTerms SummarizeType { get; protected set; }
        public TAmountDec IncomeRelated { get; protected set; }
        public TAmountDec IncomeExclude { get; protected set; }

        public IncomeInsHealthValue(WorkHealthTerms summarize, TAmountDec realted, TAmountDec exclude) : base((ResultCode)ArticleResultCode.RESULT_VALUE_INCOME_SUM_HEALTH)
        {
            this.SummarizeType = summarize;
            this.IncomeRelated = realted;
            this.IncomeExclude = exclude;
        }
        public override string Description()
        {
            return string.Format("{0}: Summarize: {1}, Income Related: {2}, Income Exclude: {3}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                SummarizeType.GetSymbol(), IncomeRelated.FormatAmount(), IncomeExclude.FormatAmount());
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = IncomeRelated.FormatAmount();
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}