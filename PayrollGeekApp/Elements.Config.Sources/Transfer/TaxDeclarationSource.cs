using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using TAmountDec = Decimal;

    using Legalist.Constants;
    using Module.Interfaces.Elements;

    public class TaxDeclarationSource : ISourceValues, ICloneable
    {
        public Byte StatementType { get; set; }
        public WorkTaxingTerms SummarizeType { get; set; }
        public Byte DeclaracyType { get; set; }
        public Byte ResidencyType { get; set; }
        public TAmountDec HealthAnnuity { get; set; }
        public TAmountDec SocialAnnuity { get; set; }

        public TaxDeclarationSource()
        {
            StatementType = 0;
            SummarizeType = WorkTaxingTerms.TAXING_TERM_EMPLOYMENT_POLICY;
            DeclaracyType = 0;
            ResidencyType = 0;
            HealthAnnuity = TAmountDec.Zero;
            SocialAnnuity = TAmountDec.Zero;
        }

        public TaxDeclarationSource(Byte statementType, WorkTaxingTerms summarizeType, Byte declaracyType, Byte residencyType, TAmountDec healthAnnuity, TAmountDec socialAnnuity)
        {
            StatementType = statementType;
            SummarizeType = summarizeType;
            DeclaracyType = declaracyType;
            ResidencyType = residencyType;
            HealthAnnuity = healthAnnuity;
            SocialAnnuity = socialAnnuity;
        }

        public virtual object Clone()
        {
            TaxDeclarationSource cloneSource = (TaxDeclarationSource)this.MemberwiseClone();

            cloneSource.StatementType = this.StatementType;
            cloneSource.SummarizeType = this.SummarizeType;
            cloneSource.DeclaracyType = this.DeclaracyType;
            cloneSource.ResidencyType = this.ResidencyType;
            cloneSource.HealthAnnuity = this.HealthAnnuity;
            cloneSource.SocialAnnuity = this.SocialAnnuity;

            return cloneSource;
        }

    }
}
