using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using BundleVersion = UInt16;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Constants;

    public abstract class TaxingGuidesBuilder : GeneralGuides, ITaxingGuidesBuilder
    {
        protected readonly TAmountInt _AllowancePayer;
        protected readonly TAmountInt _AllowanceDisab1st;
        protected readonly TAmountInt _AllowanceDisab2nd;
        protected readonly TAmountInt _AllowanceDisab3rd;
        protected readonly TAmountInt _AllowanceStudy;
        protected readonly TAmountInt _AllowanceChild1st;
        protected readonly TAmountInt _AllowanceChild2nd;
        protected readonly TAmountInt _AllowanceChild3rd;
        protected readonly TAmountDec _FactorAdvances;
        protected readonly TAmountDec _FactorWithhold;
        protected readonly TAmountDec _FactorSolidary;
        protected readonly TAmountInt _MinValidAmountOfTaxBonus;
        protected readonly TAmountInt _MaxValidAmountOfTaxBonus;
        protected readonly TAmountInt _MinValidIncomeOfTaxBonus;
        protected readonly TAmountInt _MaxValidIncomeOfRounding;
        protected readonly TAmountInt _MaxTaskAgrIncomeWithhold;
        protected readonly TAmountInt _MaxLoLevelIncomeWithhold;
        protected readonly TaxingPartnerIncome _TaxPartnerIncomeWithhold;
        protected readonly TAmountInt _MaxHealthAnnualBasisAdvance;
        protected readonly TAmountInt _MaxSocialAnnualBasisAdvance;
        protected readonly TAmountInt _MaxHealthAnnualBasisWithhold;
        protected readonly TAmountInt _MaxSocialAnnualBasisWithhold;
        protected readonly TAmountInt _MinValidIncomeOfSolidary;

        public TaxingGuidesBuilder(BundleVersion version, 
            TAmountInt allowancePayer, 
            TAmountInt allowanceDisab1st, TAmountInt allowanceDisab2nd, TAmountInt allowanceDisab3rd,
            TAmountInt allowanceStudy, 
            TAmountInt allowanceChild1st, TAmountInt allowanceChild2nd, TAmountInt allowanceChild3rd,
            TAmountDec factorAdvances, TAmountDec factorWithhold, TAmountDec factorSolidary,
            TAmountInt minValidAmountOfTaxBonus, TAmountInt maxValidAmountOfTaxBonus,
            TAmountInt minValidIncomeOfTaxBonus, TAmountInt maxValidIncomeOfRounding,
            TAmountInt maxTaskAgrIncomeWithhold, TAmountInt maxLoLevelIncomeWithhold, TaxingPartnerIncome taxPartnerIncomeWithhold,
            TAmountInt maxHealthAnnualBasisAdvance, TAmountInt maxSocialAnnualBasisAdvance,
            TAmountInt maxHealthAnnualBasisWithhold, TAmountInt maxSocialAnnualBasisWithhold,
            TAmountInt minValidIncomeOfSolidary) : base(version)
        {
            this._AllowancePayer = allowancePayer;
            this._AllowanceDisab1st = allowanceDisab1st;
            this._AllowanceDisab2nd = allowanceDisab2nd;
            this._AllowanceDisab3rd = allowanceDisab3rd;
            this._AllowanceStudy = allowanceStudy;
            this._AllowanceChild1st = allowanceChild1st;
            this._AllowanceChild2nd = allowanceChild2nd;
            this._AllowanceChild3rd = allowanceChild3rd;
            this._FactorAdvances = factorAdvances;
            this._FactorWithhold = factorWithhold;
            this._FactorSolidary = factorSolidary;
            this._MinValidAmountOfTaxBonus = minValidAmountOfTaxBonus;
            this._MaxValidAmountOfTaxBonus = maxValidAmountOfTaxBonus;
            this._MinValidIncomeOfTaxBonus = minValidIncomeOfTaxBonus;
            this._MaxValidIncomeOfRounding = maxValidIncomeOfRounding;
            this._MaxTaskAgrIncomeWithhold = maxTaskAgrIncomeWithhold;
            this._MaxLoLevelIncomeWithhold = maxLoLevelIncomeWithhold;
            this._TaxPartnerIncomeWithhold = taxPartnerIncomeWithhold;
            this._MaxHealthAnnualBasisAdvance = maxHealthAnnualBasisAdvance;
            this._MaxSocialAnnualBasisAdvance = maxSocialAnnualBasisAdvance;
            this._MaxHealthAnnualBasisWithhold = maxHealthAnnualBasisWithhold;
            this._MaxSocialAnnualBasisWithhold = maxSocialAnnualBasisWithhold;
            this._MinValidIncomeOfSolidary = minValidIncomeOfSolidary;
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public ITaxingGuides BuildPeriodGuides(Period period)
        {
            return new TaxingGuides(period,
                AllowancePayer(period),
                AllowanceDisab1st(period),
                AllowanceDisab2nd(period),
                AllowanceDisab3rd(period),
                AllowanceStudy(period),
                AllowanceChild1st(period),
                AllowanceChild2nd(period),
                AllowanceChild3rd(period),
                FactorAdvances(period),
                FactorWithhold(period),
                FactorSolidary(period),
                MinValidAmountOfTaxBonus(period),
                MaxValidAmountOfTaxBonus(period),
                MinValidIncomeOfTaxBonus(period),
                MaxValidIncomeOfRounding(period),
                MaxTaskAgrIncomeWithhold(period),
                MaxLoLevelIncomeWithhold(period),
                TaxPartnerIncomeWithhold(period),
                MaxHealthAnnualBasisAdvance(period),
                MaxSocialAnnualBasisAdvance(period),
                MaxHealthAnnualBasisWithhold(period),
                MaxSocialAnnualBasisWithhold(period),
                MinValidIncomeOfSolidary(period));
        }

        public abstract TAmountInt AllowancePayer(Period period);
        public abstract TAmountInt AllowanceDisab1st(Period period);
        public abstract TAmountInt AllowanceDisab2nd(Period period);
        public abstract TAmountInt AllowanceDisab3rd(Period period);
        public abstract TAmountInt AllowanceStudy(Period period);
        public abstract TAmountInt AllowanceChild1st(Period period);
        public abstract TAmountInt AllowanceChild2nd(Period period);
        public abstract TAmountInt AllowanceChild3rd(Period period);
        public abstract TAmountDec FactorAdvances(Period period);
        public abstract TAmountDec FactorWithhold(Period period);
        public abstract TAmountDec FactorSolidary(Period period);
        public abstract TAmountInt MinValidAmountOfTaxBonus(Period period);
        public abstract TAmountInt MaxValidAmountOfTaxBonus(Period period);
        public abstract TAmountInt MinValidIncomeOfTaxBonus(Period period);
        public abstract TAmountInt MaxValidIncomeOfRounding(Period period);
        public abstract TAmountInt MaxTaskAgrIncomeWithhold(Period period);
        public abstract TAmountInt MaxLoLevelIncomeWithhold(Period period);
        public abstract TaxingPartnerIncome TaxPartnerIncomeWithhold(Period period);
        public abstract TAmountInt MaxHealthAnnualBasisAdvance(Period period);
        public abstract TAmountInt MaxSocialAnnualBasisAdvance(Period period);
        public abstract TAmountInt MaxHealthAnnualBasisWithhold(Period period);
        public abstract TAmountInt MaxSocialAnnualBasisWithhold(Period period);
        public abstract TAmountInt MinValidIncomeOfSolidary(Period period);
    }
}
