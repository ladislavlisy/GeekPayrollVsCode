using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Taxing
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Module.Interfaces.Legalist;
    using Module.Items;
    using Operations;
    using Constants;

    public class TaxingGuides : ITaxingGuides
    {
        protected Period InternalPeriod { get; set; }
        protected TAmountInt __AllowancePayer { get; set; }
        protected TAmountInt __AllowanceDisab1st { get; set; }
        protected TAmountInt __AllowanceDisab2nd { get; set; }
        protected TAmountInt __AllowanceDisab3rd { get; set; }
        protected TAmountInt __AllowanceStudy { get; set; }
        protected TAmountInt __AllowanceChild1st { get; set; }
        protected TAmountInt __AllowanceChild2nd { get; set; }
        protected TAmountInt __AllowanceChild3rd { get; set; }
        protected TAmountDec __FactorAdvances { get; set; }
        protected TAmountDec __FactorWithhold { get; set; }
        protected TAmountDec __FactorSolidary { get; set; }
        protected TAmountInt __MinValidAmountOfTaxBonus { get; set; }
        protected TAmountInt __MaxValidAmountOfTaxBonus { get; set; }
        protected TAmountInt __MinValidIncomeOfTaxBonus { get; set; }
        protected TAmountInt __MaxValidIncomeOfRounding { get; set; }
        protected TAmountInt __MaxTaskAgrIncomeWithhold { get; set; }
        protected TAmountInt __MaxLoLevelIncomeWithhold { get; set; }
        protected TaxingPartnerIncome __TaxPartnerIncomeWithhold { get; set; }
        protected TAmountInt __MaxHealthAnnualBasisAdvance { get; set; }
        protected TAmountInt __MaxSocialAnnualBasisAdvance { get; set; }
        protected TAmountInt __MaxHealthAnnualBasisWithhold { get; set; }
        protected TAmountInt __MaxSocialAnnualBasisWithhold { get; set; }
        protected TAmountInt __MinValidIncomeOfSolidary { get; set; }

        public TaxingGuides(Period period,
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
            TAmountInt minValidIncomeOfSolidary)
        {
            this.InternalPeriod = period;
            this.__AllowancePayer = allowancePayer;
            this.__AllowanceDisab1st = allowanceDisab1st;
            this.__AllowanceDisab2nd = allowanceDisab2nd;
            this.__AllowanceDisab3rd = allowanceDisab3rd;
            this.__AllowanceStudy = allowanceStudy;
            this.__AllowanceChild1st = allowanceChild1st;
            this.__AllowanceChild2nd = allowanceChild2nd;
            this.__AllowanceChild3rd = allowanceChild3rd;
            this.__FactorAdvances = factorAdvances;
            this.__FactorWithhold = factorWithhold;
            this.__FactorSolidary = factorSolidary;
            this.__MinValidAmountOfTaxBonus = minValidAmountOfTaxBonus;
            this.__MaxValidAmountOfTaxBonus = maxValidAmountOfTaxBonus;
            this.__MinValidIncomeOfTaxBonus = minValidIncomeOfTaxBonus;
            this.__MaxValidIncomeOfRounding = maxValidIncomeOfRounding;
            this.__MaxTaskAgrIncomeWithhold = maxTaskAgrIncomeWithhold;
            this.__MaxLoLevelIncomeWithhold = maxLoLevelIncomeWithhold;
            this.__TaxPartnerIncomeWithhold = taxPartnerIncomeWithhold;
            this.__MaxHealthAnnualBasisAdvance = maxHealthAnnualBasisAdvance;
            this.__MaxSocialAnnualBasisAdvance = maxSocialAnnualBasisAdvance;
            this.__MaxHealthAnnualBasisWithhold = maxHealthAnnualBasisWithhold;
            this.__MaxSocialAnnualBasisWithhold = maxSocialAnnualBasisWithhold;
            this.__MinValidIncomeOfSolidary = minValidIncomeOfSolidary;
        }
        public TAmountInt AllowancePayer()
        {
            return __AllowancePayer;
        }
        public TAmountInt AllowanceDisab1st()
        {
            return __AllowanceDisab1st;
        }
        public TAmountInt AllowanceDisab2nd()
        {
            return __AllowanceDisab2nd;
        }
        public TAmountInt AllowanceDisab3rd()
        {
            return __AllowanceDisab3rd;
        }
        public TAmountInt AllowanceStudy()
        {
            return __AllowanceStudy;
        }
        public TAmountInt AllowanceChild1st()
        {
            return __AllowanceChild1st;
        }
        public TAmountInt AllowanceChild2nd()
        {
            return __AllowanceChild2nd;
        }
        public TAmountInt AllowanceChild3rd()
        {
            return __AllowanceChild3rd;
        }
        public TAmountDec FactorAdvances()
        {
            return __FactorAdvances;
        }
        public TAmountDec FactorWithhold()
        {
            return __FactorWithhold;
        }
        public TAmountDec FactorSolidary()
        {
            return __FactorSolidary;
        }
        public TAmountInt MinValidAmountOfTaxBonus()
        {
            return __MinValidAmountOfTaxBonus;
        }
        public TAmountInt MaxValidAmountOfTaxBonus()
        {
            return __MaxValidAmountOfTaxBonus;
        }
        public TAmountInt MinValidIncomeOfTaxBonus()
        {
            return __MinValidIncomeOfTaxBonus;
        }
        public TAmountInt MaxValidIncomeOfRounding()
        {
            return __MaxValidIncomeOfRounding;
        }
        public TAmountInt MaxTaskAgrIncomeWithhold()
        {
            return __MaxTaskAgrIncomeWithhold;
        }
        public bool TaxTaskAgrIncomeWithhold()
        {
            return (__MaxTaskAgrIncomeWithhold != 0);
        }
        public TAmountInt MaxLoLevelIncomeWithhold()
        {
            return __MaxLoLevelIncomeWithhold;
        }
        public bool TaxLoLevelIncomeWithhold()
        {
            return (__MaxLoLevelIncomeWithhold != 0);
        }
        public TaxingPartnerIncome TaxPartnerIncomeWithhold()
        {
            return __TaxPartnerIncomeWithhold;
        }
        public TAmountInt MaxHealthAnnualBasisAdvance()
        {
            return __MaxHealthAnnualBasisAdvance;
        }
        public TAmountInt MaxSocialAnnualBasisAdvance()
        {
            return __MaxSocialAnnualBasisAdvance;
        }
        public TAmountInt MaxHealthAnnualBasisWithhold()
        {
            return __MaxHealthAnnualBasisWithhold;
        }
        public TAmountInt MaxSocialAnnualBasisWithhold()
        {
            return __MaxSocialAnnualBasisWithhold;
        }

        public TAmountInt MinValidIncomeOfSolidary()
        {
            return __MinValidIncomeOfSolidary;
        }
    }
}
