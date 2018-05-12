using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Health
{
    using TAmountDec = Decimal;

    using Constants;
    using Module.Interfaces.Legalist;
    using Module.Items;
    public class HealthGuidingProfile : IHealthProfile
    {
        protected Period InternalPeriod { get; set; }
        protected IHealthGuides InternalGuides { get; set; }

        public HealthGuidingProfile(Period period, IHealthGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public IHealthGuides Guides()
        {
            return InternalGuides;
        }
        public TAmountDec FactorCompound()
        {
            return InternalGuides.FactorCompound();
        }
        public TAmountDec IncludeGeneralIncomes(Period evalPeriod, WorkHealthTerms summarize, 
            TAmountDec includeIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            switch (summarize)
            {
                case WorkHealthTerms.HEALTH_TERM_EMPLOYMENT:
                case WorkHealthTerms.HEALTH_TERM_AGREE_WORK:
                case WorkHealthTerms.HEALTH_TERM_AGREE_TASK:
                case WorkHealthTerms.HEALTH_TERM_OUT_EMPLOY:
                    totalIncome = decimal.Add(totalIncome, includeIncome);
                    break;
            }
            return totalIncome;
        }

        public TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkHealthTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            switch (summarize)
            {
                case WorkHealthTerms.HEALTH_TERM_EMPLOYMENT:
                case WorkHealthTerms.HEALTH_TERM_AGREE_WORK:
                case WorkHealthTerms.HEALTH_TERM_AGREE_TASK:
                case WorkHealthTerms.HEALTH_TERM_OUT_EMPLOY:
                    totalIncome = decimal.Add(totalIncome, excludeIncome);
                    break;
            }
            return totalIncome;
        }
    }
}
