using System;

namespace ElementsLib.Elements.Config.Results
{
    using TDay = Byte;
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using Legalist.Constants;

    public class DeclarationHealthValue : GeneralResultValue
    {
        public Byte StatementType { get; protected set; }
        public WorkHealthTerms SummarizeType { get; protected set; }
        public Byte ForeignerType { get; protected set; }
        public TAmountDec HealthAnnuity { get; protected set; }

        public DeclarationHealthValue(Byte statement, WorkHealthTerms summarize, TAmountDec totalBase, Byte foreigner) : base((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_HEALTH)
        {
            this.StatementType = statement;
            this.SummarizeType = summarize;
            this.ForeignerType = foreigner;
            this.HealthAnnuity = totalBase;
        }
        public override string Description()
        {
            return string.Format("{0}: Statement: {1}, Summarize: {2}, Foreigner: {3}, Health Annuity: {4}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                StatementType.ToString(), SummarizeType.GetSymbol(), ForeignerType.ToString(), HealthAnnuity.FormatAmount());
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = HealthAnnuity.FormatAmount();
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}