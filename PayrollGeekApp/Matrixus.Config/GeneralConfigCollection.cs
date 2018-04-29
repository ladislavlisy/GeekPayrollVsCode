using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementsLib.Elements.Config
{
    using SymbolName = String;

    public abstract class GeneralConfigCollection<TConfig, TIndex>
    {
        public GeneralConfigCollection()
        {
            this.InternalModels = new Dictionary<TIndex, TConfig>();
        }

        protected IDictionary<TIndex, TConfig> InternalModels { get; set; }
        protected TIndex DefaultCode { get; set; }

        protected void ConfigureModel(IEnumerable<KeyValuePair<TIndex, TConfig>> configList)
        {
            InternalModels = configList.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        protected TConfig FindConfigByCode(TIndex configCode)
        {
            TConfig modelInstance = default(TConfig);

            if (InternalModels.ContainsKey(configCode))
            {
                modelInstance = InternalModels[configCode];
            }
            else
            {
                modelInstance = InternalModels[DefaultCode];
            }
            return modelInstance;
        }
    }
}
