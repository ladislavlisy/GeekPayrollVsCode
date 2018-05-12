using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Health
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class HealthGuides : IHealthGuides
    {
        protected Period InternalPeriod { get; set; }
        protected TAmountInt __BasisMonthlyMinimum { get; set; }
        protected TAmountDec __BasisAnnualMaximum { get; set; }
        protected TAmountDec __FactorCompound { get; set; }
        protected TAmountDec __IncomeEmployMargin { get; set; }
        protected TAmountDec __IncomeAgreemMargin { get; set; }


        public HealthGuides(Period period,
            TAmountInt basisMonthlyMinimum, TAmountDec basisAnnualMaximum, TAmountDec factorCompound,
            TAmountDec incomeEmployMargin, TAmountDec incomeAgreemMargin)
        {
            this.InternalPeriod = period;
            this.__BasisMonthlyMinimum = basisMonthlyMinimum;
            this.__BasisAnnualMaximum = basisAnnualMaximum;
            this.__FactorCompound = factorCompound;
            this.__IncomeEmployMargin = incomeEmployMargin;
            this.__IncomeAgreemMargin = incomeAgreemMargin;
        }
        public TAmountInt BasisMonthlyMinimum()
        {
            return __BasisMonthlyMinimum;
        }
        public TAmountDec BasisAnnualMaximum()
        {
            return __BasisAnnualMaximum;
        }
        public TAmountDec FactorCompound()
        {
            return __FactorCompound;
        }
        public TAmountDec IncomeEmployMargin()
        {
            return __IncomeEmployMargin;
        }
        public TAmountDec IncomeAgreemMargin()
        {
            return __IncomeAgreemMargin;
        }
    }
}
