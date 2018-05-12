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

    public class InsDeclarationHealthSource : ISourceValues, ICloneable
    {
        public Byte StatementType { get; set; }
        public WorkHealthTerms SummarizeType { get; set; }
        public TAmountDec TotalYearBase { get; set; }

        public InsDeclarationHealthSource()
        {
            StatementType = 0;
            SummarizeType = WorkHealthTerms.HEALTH_TERM_EMPLOYMENT;
            TotalYearBase = decimal.Zero;
        }

        public InsDeclarationHealthSource(Byte statementType, WorkHealthTerms summarizeType, TAmountDec totalYearBase)
        {
            StatementType = statementType;
            SummarizeType = summarizeType;
            TotalYearBase = totalYearBase;
        }

        public virtual object Clone()
        {
            InsDeclarationHealthSource cloneSource = (InsDeclarationHealthSource)this.MemberwiseClone();

            cloneSource.StatementType = this.StatementType;
            cloneSource.SummarizeType = this.SummarizeType;
            cloneSource.TotalYearBase = this.TotalYearBase;

            return cloneSource;
        }

    }
}
