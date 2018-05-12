using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Taxing
{
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Constants;
    using Module.Interfaces.Legalist;
    using Module.Items;
    using Rounding;
    using Operations;

    public class TaxingGuidingProfile : ITaxingProfile
    {
        public const TAmountInt NUMBER_ONE_HUNDRED = 100;
        public const TAmountDec NUMBER_100_PERCENT = 100m;
        protected Period InternalPeriod { get; set; }
        protected ITaxingGuides InternalGuides { get; set; }

        public TaxingGuidingProfile(Period period, ITaxingGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public ITaxingGuides Guides()
        {
            return InternalGuides;
        }

        public TAmountDec DecRoundUp(TAmountDec valueDec)
        {
            return RoundingDec.RoundUp(valueDec);
        }

        public TAmountInt IntRoundUp(TAmountDec valueDec)
        {
            return RoundingInt.RoundUp(valueDec);
        }

        public TAmountDec DecRoundDown(TAmountDec valueDec)
        {
            return RoundingDec.RoundDown(valueDec);
        }

        public TAmountInt IntRoundDown(TAmountDec valueDec)
        {
            return RoundingInt.RoundDown(valueDec);
        }

        public TAmountDec DecRoundUpHundreds(TAmountDec valueDec)
        {
            return RoundingDec.NearRoundUp(valueDec, NUMBER_ONE_HUNDRED);
        }

        public TAmountDec DecFactorResult(TAmountDec valueDec, TAmountDec factor)
        {
            return OperationsDec.MultiplyAndDivide(valueDec, factor, NUMBER_100_PERCENT);
        }

        public TAmountInt RebateResult(TAmountDec rebateBasis, TAmountDec rebateApply, TAmountDec rebateClaim)
        {
            TAmountDec afterApply = TAmountDec.Subtract(rebateBasis, rebateApply);

            TAmountDec afterClaim = Math.Max(0m, TAmountDec.Subtract(rebateClaim, afterApply));

            TAmountDec rebateResult = TAmountDec.Subtract(rebateClaim, afterClaim);

            return RoundingInt.RoundToInt(rebateResult);
        }

        public TAmountDec TaxableGeneralIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (statement == TaxStatement.TAXABLE)
            {
                if (declaracy == TaxDeclaracy.SIGNED)
                {
                    totalIncome = decimal.Add(totalIncome, taxableIncome);
                    totalIncome = decimal.Add(totalIncome, partnerIncome);
                }
            }
            return totalIncome;
        }

        public TAmountDec TaxableLolevelIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (statement == TaxStatement.TAXABLE)
            {
                if (declaracy == TaxDeclaracy.NONSIGNED)
                {
                    switch (summarize)
                    {
                        case WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY:
                            totalIncome = decimal.Add(totalIncome, taxableIncome);
                            break;
                        case WorkTaxingTerms.TAXING_TERM_FOR_TASK_AGREEMENT:
                            break;
                        case WorkTaxingTerms.TAXING_TERM_STATUTORY_PARTNER:
                            break;
                    }
                }
            }
            return totalIncome;
        }

        public TAmountDec TaxableAgrWorkIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (statement == TaxStatement.TAXABLE)
            {
                if (declaracy == TaxDeclaracy.NONSIGNED)
                {
                    switch (summarize)
                    {
                        case WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY:
                            break;
                        case WorkTaxingTerms.TAXING_TERM_FOR_TASK_AGREEMENT:
                            totalIncome = decimal.Add(totalIncome, taxableIncome);
                            break;
                        case WorkTaxingTerms.TAXING_TERM_STATUTORY_PARTNER:
                            break;
                    }
                }
            }
            return totalIncome;
        }

        public TAmountDec TaxablePartnerIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (statement == TaxStatement.TAXABLE)
            {
                if (InternalGuides.TaxPartnerIncomeWithhold() == TaxingPartnerIncome.TAXING_WITHHOLD)
                {
                    switch (summarize)
                    {
                        case WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY:
                        case WorkTaxingTerms.TAXING_TERM_FOR_TASK_AGREEMENT:
                            totalIncome = decimal.Add(totalIncome, partnerIncome);
                            break;
                        case WorkTaxingTerms.TAXING_TERM_STATUTORY_PARTNER:
                            totalIncome = decimal.Add(totalIncome, taxableIncome);
                            totalIncome = decimal.Add(totalIncome, partnerIncome);
                            break;
                    }
                }
                else if (InternalGuides.TaxPartnerIncomeWithhold() == TaxingPartnerIncome.NONSIGNED_WITHHOLD)
                {
                    if (declaracy == TaxDeclaracy.NONSIGNED)
                    {
                        switch (summarize)
                        {
                            case WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY:
                            case WorkTaxingTerms.TAXING_TERM_FOR_TASK_AGREEMENT:
                                totalIncome = decimal.Add(totalIncome, partnerIncome);
                                break;
                            case WorkTaxingTerms.TAXING_TERM_STATUTORY_PARTNER:
                                totalIncome = decimal.Add(totalIncome, taxableIncome);
                                totalIncome = decimal.Add(totalIncome, partnerIncome);
                                break;
                        }
                    }
                }
            }
            return totalIncome;
        }

        public TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkTaxingTerms summarize,
            Byte statement, Byte declaracy, Byte residency,
            TAmountDec taxableIncome, TAmountDec partnerIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (statement == TaxStatement.NONTAXABLE)
            {
                totalIncome = decimal.Add(totalIncome, taxableIncome);
                totalIncome = decimal.Add(totalIncome, partnerIncome);
                totalIncome = decimal.Add(totalIncome, excludeIncome);
            }
            else if (statement == TaxStatement.TAXABLE)
            {
                totalIncome = decimal.Add(totalIncome, excludeIncome);
            }
            return totalIncome;
        }

        public TAmountDec TaxableIncomesAdvanceTaxingMode(Period evalPeriod,
            TAmountDec generalIncome, TAmountDec excludeIncome,
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            totalIncome = decimal.Add(totalIncome, generalIncome);
            if (InternalGuides.TaxLoLevelIncomeWithhold()==false)
            {
                totalIncome = decimal.Add(totalIncome, lolevelIncome);
            }
            else if (InternalGuides.MaxLoLevelIncomeWithhold() > lolevelIncome)
            {
                totalIncome = decimal.Add(totalIncome, lolevelIncome);
            }
            if (InternalGuides.TaxTaskAgrIncomeWithhold()==false)
            {
                totalIncome = decimal.Add(totalIncome, agrtaskIncome);
            }
            else if (InternalGuides.MaxTaskAgrIncomeWithhold() > agrtaskIncome)
            {
                totalIncome = decimal.Add(totalIncome, agrtaskIncome);
            }
            return totalIncome;
        }
        public TAmountDec TaxableIncomesWithholdLolevelMode(Period evalPeriod,
            TAmountDec generalIncome, TAmountDec excludeIncome,
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (InternalGuides.TaxLoLevelIncomeWithhold())
            {
                if (lolevelIncome > 0 && InternalGuides.MaxLoLevelIncomeWithhold() <= lolevelIncome)
                {
                    totalIncome = decimal.Add(totalIncome, lolevelIncome);
                }
            }
            return totalIncome;
        }

        public TAmountDec TaxableIncomesWithholdTaskAgrMode(Period evalPeriod,
            TAmountDec generalIncome, TAmountDec excludeIncome,
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (InternalGuides.TaxTaskAgrIncomeWithhold())
            {
                if (agrtaskIncome > 0 && InternalGuides.MaxTaskAgrIncomeWithhold() <= agrtaskIncome)
                {
                    totalIncome = decimal.Add(totalIncome, agrtaskIncome);
                }
            }
            return totalIncome;
        }

        public TAmountDec TaxableIncomesWithholdPartnerMode(Period evalPeriod,
            TAmountDec generalIncome, TAmountDec excludeIncome,
            TAmountDec lolevelIncome, TAmountDec agrtaskIncome, TAmountDec partnerIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            if (InternalGuides.TaxPartnerIncomeWithhold()!=TaxingPartnerIncome.TAXING_ADVANCE)
            {
                totalIncome = decimal.Add(totalIncome, partnerIncome);
            }
            return totalIncome;
        }
        public TAmountDec TaxableBaseAdvanceTaxingMode(Period evalPeriod, TAmountDec generalIncome)
        {
            return generalIncome;
        }
        public TAmountDec TaxableBaseWithholdTaxingMode(Period evalPeriod, TAmountDec generalIncome)
        {
            return generalIncome;
        }
        public TAmountDec TaxablePartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome)
        {
            TAmountDec annuityBaseLimit = InternalGuides.MaxHealthAnnualBasisAdvance();
            TAmountDec cutdownBaseValue = OperationsDec.MaxDecAccumValue(generalIncome, annuityIncome, annuityBaseLimit);
            return cutdownBaseValue;
        }
        public TAmountDec CutDownPartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome)
        {
            TAmountDec annuityBaseLimit = InternalGuides.MaxHealthAnnualBasisAdvance();
            TAmountDec cutdownBaseAbove = OperationsDec.MaxDecAccumAbove(generalIncome, annuityIncome, annuityBaseLimit);
            return cutdownBaseAbove;
        }
        private TAmountInt CompoundPartialWithHealthFactor(decimal compoundBasis, decimal compoundFactor)
        {
            TAmountDec decimalResult = DecFactorResult(compoundBasis, compoundFactor);

            TAmountInt roundedResult = IntRoundUp(decimalResult);

            return roundedResult;
        }
        private TAmountInt EmployeePartialWithHealthFactor(decimal compoundBasis, decimal compoundFactor)
        {
            TAmountDec decimalResult = DecFactorResult(compoundBasis, compoundFactor);

            TAmountDec thirdedResult = OperationsDec.Divide(decimalResult, 3);

            TAmountInt roundedResult = IntRoundUp(thirdedResult);

            return roundedResult;
        }
        public TAmountDec EployerPartialAdvanceHealth(Period evalPeriod, TAmountDec generalIncome, TAmountDec compoundFactor)
        {
            Int32 compoundPartialValue = CompoundPartialWithHealthFactor(generalIncome, compoundFactor);

            Int32 employeePartialValue = EmployeePartialWithHealthFactor(generalIncome, compoundFactor);

            Int32 employerPartialValue = (compoundPartialValue - employeePartialValue);

            return employerPartialValue;
        }
        public TAmountDec TaxablePartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome)
        {
            TAmountDec annuityBaseLimit = InternalGuides.MaxSocialAnnualBasisAdvance();
            TAmountDec cutdownBaseValue = OperationsDec.MaxDecAccumValue(generalIncome, annuityIncome, annuityBaseLimit);
            return cutdownBaseValue;
        }
        public TAmountDec CutDownPartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec annuityIncome)
        {
            TAmountDec annuityBaseLimit = InternalGuides.MaxSocialAnnualBasisAdvance();
            TAmountDec cutdownBaseAbove = OperationsDec.MaxDecAccumAbove(generalIncome, annuityIncome, annuityBaseLimit);
            return cutdownBaseAbove;
        }
        private TAmountInt EmployerPartialWithSocialFactor(decimal employerBasis, decimal employerFactor)
        {
            TAmountDec decimalResult = DecFactorResult(employerBasis, employerFactor);

            TAmountInt roundedResult = IntRoundUp(decimalResult);

            return roundedResult;
        }
        public TAmountDec EployerPartialAdvanceSocial(Period evalPeriod, TAmountDec generalIncome, TAmountDec employerFactor)
        {
            Int32 employerPartialValue = EmployerPartialWithSocialFactor(generalIncome, employerFactor);

            return employerPartialValue;
        }
        public TAmountDec BasisSolidaryRounded(TAmountDec generalIncome)
        {
            decimal solidaryLimit = InternalGuides.MinValidIncomeOfSolidary();

            decimal solidaryBasis = Math.Max(0, generalIncome - solidaryLimit);

            return solidaryBasis;
        }
    }
}
