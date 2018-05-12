using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Versions.Social
{
    using TAmountDec = Decimal;

    using Constants;
    using Module.Interfaces.Legalist;
    using Module.Items;
    public class SocialGuidingProfile : ISocialProfile
    {
        protected Period InternalPeriod { get; set; }
        protected ISocialGuides InternalGuides { get; set; }

        public SocialGuidingProfile(Period period, ISocialGuides guides)
        {
            this.InternalPeriod = period;

            this.InternalGuides = guides;
        }

        public ISocialGuides Guides()
        {
            return InternalGuides;
        }
        public TAmountDec FactorEmployer()
        {
            return InternalGuides.FactorEmployer();
        }
        public TAmountDec IncludeGeneralIncomes(Period evalPeriod, WorkSocialTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            switch (summarize)
            {
                case WorkSocialTerms.SOCIAL_TERM_EMPLOYMENT:
                case WorkSocialTerms.SOCIAL_TERM_SMALL_EMPL:
                case WorkSocialTerms.SOCIAL_TERM_SHORT_MEET:
                case WorkSocialTerms.SOCIAL_TERM_SHORT_DENY:
                    totalIncome = decimal.Add(totalIncome, includeIncome);
                    break;
            }
            return totalIncome;
        }

        public TAmountDec ExcludeGeneralIncomes(Period evalPeriod, WorkSocialTerms summarize,
            TAmountDec includeIncome, TAmountDec excludeIncome)
        {
            TAmountDec totalIncome = decimal.Zero;
            switch (summarize)
            {
                case WorkSocialTerms.SOCIAL_TERM_EMPLOYMENT:
                case WorkSocialTerms.SOCIAL_TERM_SMALL_EMPL:
                case WorkSocialTerms.SOCIAL_TERM_SHORT_MEET:
                case WorkSocialTerms.SOCIAL_TERM_SHORT_DENY:
                    totalIncome = decimal.Add(totalIncome, excludeIncome);
                    break;
            }
            return totalIncome;
        }
    }
}
