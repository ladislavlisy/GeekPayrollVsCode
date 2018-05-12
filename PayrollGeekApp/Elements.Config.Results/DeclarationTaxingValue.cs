using System;

namespace ElementsLib.Elements.Config.Results
{
    using TDay = Byte;
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using Legalist.Constants;

    public class DeclarationTaxingValue : GeneralResultValue
    {
        public Byte StatementType { get; protected set; }
        public WorkTaxingTerms SummarizeType { get; protected set; }
        public Byte DeclaracyType { get; protected set; }
        public Byte ResidencyType { get; protected set; }
        public TAmountDec HealthAnnuity { get; protected set; }
        public TAmountDec SocialAnnuity { get; protected set; }

        public DeclarationTaxingValue(Byte statement, WorkTaxingTerms summarize, Byte declaracy, Byte residency, TAmountDec healthSum, TAmountDec socialSum) : base((ResultCode)ArticleResultCode.RESULT_VALUE_DECLARATION_TAXING)
        {
            this.StatementType = statement;
            this.SummarizeType = summarize;
            this.DeclaracyType = declaracy;
            this.ResidencyType = residency;
            this.HealthAnnuity = healthSum;
            this.SocialAnnuity = socialSum;

        }
        public override string Description()
        {
            return string.Format("{0}: Statement: {1}, Summarize: {2}, Declaration: {3}, Residency: {4}, Health Annuity: {5}, Social Annuity: {5}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(),
                StatementType.ToString(), SummarizeType.GetSymbol(), DeclaracyType.ToString(), ResidencyType.ToString(),
                HealthAnnuity.FormatAmount(), SocialAnnuity.FormatAmount());
        }
        public override string ToResultExport(string targetSymbol)
        {
            TAmountDec TotalsAnnuity = decimal.Add(HealthAnnuity, SocialAnnuity);

            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = TotalsAnnuity.FormatAmount();
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}