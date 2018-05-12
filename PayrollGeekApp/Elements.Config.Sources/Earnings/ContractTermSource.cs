using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Config.Sources
{
    using Legalist.Constants;
    using Module.Interfaces.Elements;
    using Module.Json;
    using Module.Libs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ContractTermSource : ISourceValues, ICloneable
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateStop { get; set; }
        public WorkEmployTerms ContractType { get; set; }

        public ContractTermSource()
        {
            DateFrom = null;
            DateStop = null;
            ContractType = WorkEmployTerms.WORKTERM_UNKNOWN_TYPE;
        }

        public ContractTermSource(DateTime? dateFrom, DateTime? dateStop, WorkEmployTerms contractType)
        {
            DateFrom = dateFrom;
            DateStop = dateStop;
            ContractType = contractType;
        }

        public virtual object Clone()
        {
            ContractTermSource cloneSource = (ContractTermSource)this.MemberwiseClone();

            cloneSource.DateFrom = this.DateFrom;
            cloneSource.DateStop = this.DateStop;
            cloneSource.ContractType = this.ContractType;

            return cloneSource;
        }

    }
}
