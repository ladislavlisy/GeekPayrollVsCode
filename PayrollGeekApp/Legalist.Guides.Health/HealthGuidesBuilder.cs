using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Health
{
    using BundleVersion = UInt16;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class HealthGuidesBuilder : GeneralGuides, IHealthGuidesBuilder
    { 
        protected readonly TAmountInt _BasisMonthlyMinimum;
        protected readonly TAmountDec _BasisAnnualMaximum;
        protected readonly TAmountDec _FactorCompound;
        protected readonly TAmountDec _IncomeEmployMargin;
        protected readonly TAmountDec _IncomeAgreemMargin;

        public HealthGuidesBuilder(BundleVersion version, 
            TAmountInt basisMonthlyMinimum, TAmountDec basisAnnualMaximum, TAmountDec factorCompound,
            TAmountDec incomeEmployMargin, TAmountDec incomeAgreemMargin) : base(version)
        {
            this._BasisMonthlyMinimum = basisMonthlyMinimum;
            this._BasisAnnualMaximum = basisAnnualMaximum;
            this._FactorCompound = factorCompound;
            this._IncomeEmployMargin = incomeEmployMargin;
            this._IncomeAgreemMargin = incomeAgreemMargin;
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public IHealthGuides BuildPeriodGuides(Period period)
        {
            return new HealthGuides(period,
                BasisMonthlyMinimum(period),
                BasisAnnualMaximum(period),
                FactorCompound(period),
                IncomeEmployMargin(period),
                IncomeAgreemMargin(period));
        }

        public abstract TAmountInt BasisMonthlyMinimum(Period period);
        public abstract TAmountDec BasisAnnualMaximum(Period period);
        public abstract TAmountDec FactorCompound(Period period);
        public abstract TAmountDec IncomeEmployMargin(Period period);
        public abstract TAmountDec IncomeAgreemMargin(Period period);
    }
}
