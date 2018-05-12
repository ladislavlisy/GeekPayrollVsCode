using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Social
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;

    public class SocialGuides : ISocialGuides
    {
        protected Period InternalPeriod { get; set; }
        protected TAmountInt __BasisMonthlyMinimum { get; set; }
        protected TAmountDec __BasisAnnualMaximum { get; set; }
        protected TAmountDec __FactorEmployer { get; set; }
        protected TAmountDec __FactorEmployerHigher { get; set; }
        protected TAmountDec __FactorEmployee { get; set; }
        protected TAmountDec __FactorEmployeeGarant { get; set; }
        protected TAmountDec __FactorReduceGarant { get; set; }
        protected TAmountDec __IncomeEmployMargin { get; set; }
        protected TAmountDec __IncomeAgreemMargin { get; set; }

        public SocialGuides(Period period, TAmountInt basisMonthlyMinimum,
            TAmountDec basisAnnualMaximum,
            TAmountDec factorEmployer, TAmountDec factorEmployerHigher,
            TAmountDec factorEmployee, TAmountDec factorEmployeeGarant,
            TAmountDec factorReduceGarant,
            TAmountDec incomeEmployMargin, TAmountDec incomeAgreemMargin)
        {
            this.InternalPeriod = period;
            this.__BasisMonthlyMinimum = basisMonthlyMinimum;
            this.__BasisAnnualMaximum = basisAnnualMaximum;
            this.__FactorEmployer = factorEmployer;
            this.__FactorEmployerHigher = factorEmployerHigher;
            this.__FactorEmployee = factorEmployee;
            this.__FactorEmployeeGarant = factorEmployeeGarant;
            this.__FactorReduceGarant = factorReduceGarant;
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
        public TAmountDec FactorEmployer()
        {
            return __FactorEmployer;
        }
        public TAmountDec FactorEmployerHigher()
        {
            return __FactorEmployerHigher;
        }
        public TAmountDec FactorEmployee()
        {
            return __FactorEmployee;
        }
        public TAmountDec FactorEmployeeGarant()
        {
            return __FactorEmployeeGarant;
        }
        public TAmountDec FactorReduceGarant()
        {
            return __FactorReduceGarant;
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
