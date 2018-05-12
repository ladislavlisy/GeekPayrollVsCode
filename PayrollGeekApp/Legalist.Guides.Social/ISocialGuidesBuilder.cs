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

    using Module.Interfaces.Legalist;
    using Module.Items;

    public interface ISocialGuidesBuilder
    {
        BundleVersion BuilderVersion();
        ISocialGuides BuildPeriodGuides(Period period);

        TAmountInt BasisMonthlyMinimum(Period period);
        TAmountDec BasisAnnualMaximum(Period period);
        TAmountDec FactorEmployer(Period period);
        TAmountDec FactorEmployerHigher(Period period);
        TAmountDec FactorEmployee(Period period);
        TAmountDec FactorEmployeeGarant(Period period);
        TAmountDec FactorReduceGarant(Period period);
        TAmountDec IncomeEmployMargin(Period period);
        TAmountDec IncomeAgreemMargin(Period period);
    }
}
