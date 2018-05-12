using System;

namespace ElementsLib.Elements.Config.Results
{
    using TAmountDec = Decimal;
    using ResultCode = UInt16;

    using Module.Libs;
    using Legalist.Constants;

    public class IncomeTaxGeneralValue : GeneralResultValue
    {
        public WorkTaxingTerms SummarizeType { get; protected set; }
        public Byte StatementType { get; protected set; }
        public Byte ResidencyType { get; protected set; } 
        public TAmountDec IncomeGeneral { get; protected set; }
        public TAmountDec IncomeExclude { get; protected set; }
        public TAmountDec IncomeLolevel { get; protected set; }
        public TAmountDec IncomeTaskAgr { get; protected set; }
        public TAmountDec IncomePartner { get; protected set; }

        public IncomeTaxGeneralValue(WorkTaxingTerms summarize, Byte statement, Byte residency, 
            TAmountDec general, TAmountDec exclude, TAmountDec lolevel, TAmountDec agrtask, TAmountDec partner) : base((ResultCode)ArticleResultCode.RESULT_VALUE_INCOME_SUM_TAXING)
        {
            this.SummarizeType = summarize;
            this.StatementType = statement;
            this.ResidencyType = residency;

            this.IncomeGeneral = general;
            this.IncomeExclude = exclude;
            this.IncomeLolevel = lolevel;
            this.IncomeTaskAgr = agrtask;
            this.IncomePartner = partner;
        }

        public override string Description()
        {
            return string.Format("{0}: Summarize: {1}, Income General: {2}, Income LoLevel: {3}, Income TaskAgr: {4}, Income Partner: {5}, Income Exclude: {6}",
                Code.ToEnum<ArticleResultCode>().GetSymbol(), SummarizeType.GetSymbol(), 
                IncomeGeneral.FormatAmount(), 
                IncomeLolevel.FormatAmount(), IncomeTaskAgr.FormatAmount(), IncomePartner.FormatAmount(), 
                IncomeExclude.FormatAmount());
        }
        public override string ToResultExport(string targetSymbol)
        {
            string hoursFormated = "";
            string dayesFormated = "";
            string moneyFormated = IncomeGeneral.FormatAmount();
            string basisFormated = "";
            string payeeFormated = "";

            return string.Format("{0}\t{1}\tHours\t{2}\tDays\t{3}\tIncome Amount\t{4}\tBasis Amount\t{5}\tPayment\t{6}",
                targetSymbol, Code.ToEnum<ArticleResultCode>().GetSymbol(),
                hoursFormated, dayesFormated, moneyFormated, basisFormated, payeeFormated);
        }
    }
}