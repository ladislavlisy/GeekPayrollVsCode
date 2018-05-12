using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Social
{
    using BundleVersion = UInt16;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class SocialGuidesBuilder : GeneralGuides, ISocialGuidesBuilder
    {
        protected readonly TAmountInt _BasisMonthlyMinimum;
        protected readonly TAmountDec _BasisAnnualMaximum;
        protected readonly TAmountDec _FactorEmployer;
        protected readonly TAmountDec _FactorEmployerHigher;
        protected readonly TAmountDec _FactorEmployee;
        protected readonly TAmountDec _FactorEmployeeGarant;
        protected readonly TAmountDec _FactorReduceGarant;
        protected readonly TAmountDec _IncomeEmployMargin;
        protected readonly TAmountDec _IncomeAgreemMargin;

        public SocialGuidesBuilder(BundleVersion version, TAmountInt basisMonthlyMinimum,
            TAmountDec basisAnnualMaximum,
            TAmountDec factorEmployer, TAmountDec factorEmployerHigher,
            TAmountDec factorEmployee, TAmountDec factorEmployeeGarant,
            TAmountDec factorReduceGarant,
            TAmountDec incomeEmployMargin, TAmountDec incomeAgreemMargin) : base(version)       
        {
            this._BasisMonthlyMinimum = basisMonthlyMinimum;
            this._BasisAnnualMaximum = basisAnnualMaximum;
            this._FactorEmployer = factorEmployer;
            this._FactorEmployerHigher = factorEmployerHigher;
            this._FactorEmployee = factorEmployee;
            this._FactorEmployeeGarant = factorEmployeeGarant;
            this._FactorReduceGarant = factorReduceGarant;
            this._IncomeEmployMargin = incomeEmployMargin;
            this._IncomeAgreemMargin = incomeAgreemMargin;
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public ISocialGuides BuildPeriodGuides(Period period)
        {
            return new SocialGuides(period, 
                BasisMonthlyMinimum(period),
                BasisAnnualMaximum(period),
                FactorEmployer(period),
                FactorEmployerHigher(period),
                FactorEmployee(period),
                FactorEmployeeGarant(period),
                FactorReduceGarant(period),
                IncomeEmployMargin(period),
                IncomeAgreemMargin(period));
        }

        public abstract TAmountInt BasisMonthlyMinimum(Period period);
        public abstract TAmountDec BasisAnnualMaximum(Period period);
        public abstract TAmountDec FactorEmployer(Period period);
        public abstract TAmountDec FactorEmployerHigher(Period period);
        public abstract TAmountDec FactorEmployee(Period period);
        public abstract TAmountDec FactorEmployeeGarant(Period period);
        public abstract TAmountDec FactorReduceGarant(Period period);
        public abstract TAmountDec IncomeEmployMargin(Period period);
        public abstract TAmountDec IncomeAgreemMargin(Period period);
    }
}
