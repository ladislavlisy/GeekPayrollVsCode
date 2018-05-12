using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Items
{
    using TAmountDec = Decimal;
    public class TaxableIncomeSum
    {
        protected TAmountDec InternalIncomeGeneral { get; set; }
        protected TAmountDec InternalIncomeExclude { get; set; }
        protected TAmountDec InternalIncomeLolevel { get; set; }
        protected TAmountDec InternalIncomeTaskAgr { get; set; }
        protected TAmountDec InternalIncomePartner { get; set; }

        public TaxableIncomeSum()
        {
            this.InternalIncomeGeneral = TAmountDec.Zero;
            this.InternalIncomeExclude = TAmountDec.Zero;
            this.InternalIncomeLolevel = TAmountDec.Zero;
            this.InternalIncomeTaskAgr = TAmountDec.Zero;
            this.InternalIncomePartner = TAmountDec.Zero;
        }
        public TaxableIncomeSum(TAmountDec general, TAmountDec exclude, TAmountDec lolevel, TAmountDec agrtask, TAmountDec partner)
        {
            this.InternalIncomeGeneral = general;
            this.InternalIncomeExclude = exclude;
            this.InternalIncomeLolevel = lolevel;
            this.InternalIncomeTaskAgr = agrtask;
            this.InternalIncomePartner = partner;
        }
        public TAmountDec IncomeGeneral()
        {
            return InternalIncomeGeneral;
        }
        public TAmountDec IncomeExclude()
        {
            return InternalIncomeExclude;
        }
        public TAmountDec IncomeLolevel()
        {
            return InternalIncomeLolevel;
        }
        public TAmountDec IncomeTaskAgr()
        {
            return InternalIncomeTaskAgr;
        }
        public TAmountDec IncomePartner()
        {
            return InternalIncomePartner;
        }
        public TaxableIncomeSum Aggregate(TAmountDec general, TAmountDec exclude, TAmountDec lolevel, TAmountDec agrtask, TAmountDec partner)
        {
            return new TaxableIncomeSum(InternalIncomeGeneral + general, InternalIncomeExclude + exclude, 
                InternalIncomeLolevel + lolevel, InternalIncomeTaskAgr + agrtask, InternalIncomePartner + partner);
        }
    }
}
