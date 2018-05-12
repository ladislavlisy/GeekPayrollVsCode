using System;

namespace ElementsLib.Elements.Config.Results
{
    using TDay = Byte;
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using Legalist.Constants;

    public class DeclarationSocialValue : GeneralResultValue
    {
        public Byte StatementType { get; protected set; }
        public WorkSocialTerms SummarizeType { get; protected set; }
        public Byte ForeignerType { get; protected set; }
        public TAmountDec SocialAnnuity { get; protected set; }

        public DeclarationSocialValue(Byte statement, WorkSocialTerms summarize, TAmountDec totalBase, Byte foreigner) : base((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_SOCIAL)
        {
            this.StatementType = statement;
            this.SummarizeType = summarize;
            this.ForeignerType = foreigner;
            this.SocialAnnuity = totalBase;
        }
        public override string Description()
        {
            return string.Format("{0}: Statement: {1}, Summarize: {2}, Foreigner: {3}, Social Annuity: {4}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                StatementType.ToString(), SummarizeType.GetSymbol(), ForeignerType.ToString(), SocialAnnuity.FormatAmount());
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = SocialAnnuity.FormatAmount();
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}