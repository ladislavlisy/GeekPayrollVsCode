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

    public class InsDeclarationSocialSource : ISourceValues, ICloneable
    {
        public Byte StatementType { get; set; }
        public WorkSocialTerms SummarizeType { get; set; }
        public TAmountDec TotalYearBase { get; set; }

        public InsDeclarationSocialSource()
        {
            StatementType = 0;
            SummarizeType = WorkSocialTerms.SOCIAL_TERM_EMPLOYMENT;
            TotalYearBase = decimal.Zero;
        }

        public InsDeclarationSocialSource(Byte statementType, WorkSocialTerms summarizeType, TAmountDec totalYearBase)
        {
            StatementType = statementType;
            SummarizeType = summarizeType;
            TotalYearBase = totalYearBase;
        }

        public virtual object Clone()
        {
            InsDeclarationSocialSource cloneSource = (InsDeclarationSocialSource)this.MemberwiseClone();

            cloneSource.StatementType = this.StatementType;
            cloneSource.SummarizeType = this.SummarizeType;
            cloneSource.TotalYearBase = this.TotalYearBase;

            return cloneSource;
        }

    }
}
