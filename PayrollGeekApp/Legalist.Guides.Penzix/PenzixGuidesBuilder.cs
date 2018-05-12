using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Guides.Penzix
{
    using BundleVersion = UInt16;
    using TAmountDec = Decimal;
    using TAmountInt = Int32;

    using Operations;
    using Module.Interfaces.Legalist;
    using Module.Items;

    public abstract class PenzixGuidesBuilder : GeneralGuides, IPenzixGuidesBuilder
    {
        protected readonly TAmountDec _FactorEmployee;

        public PenzixGuidesBuilder(BundleVersion version, TAmountDec factorEmployee) : base(version)
        {
            this._FactorEmployee = factorEmployee;
        }

        public BundleVersion BuilderVersion()
        {
            return InternalVersion;
        }

        public IPenzixGuides BuildPeriodGuides(Period period)
        {
            return new PenzixGuides(period, FactorEmployee(period));
        }

        public abstract TAmountDec FactorEmployee(Period period);
    }
}
