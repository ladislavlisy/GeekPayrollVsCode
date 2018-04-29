using System;

namespace ElementsLib.Service.Calculus
{
    using SymbolUtil = Module.Codes.ArticleCzCodeUtil;

    using Module.Interfaces.Matrixus;

    public class SimpleCalculusService : CalculusService
    {
        public SimpleCalculusService(IArticleConfigProfile configProfile) : base(configProfile)
        {
            ModuleAssembly = typeof(CalculusService).Assembly;

            ContractCode = SymbolUtil.GetContractCode();

            PositionCode = SymbolUtil.GetPositionCode();
        }

        public void InitializeService()
        {
            Initialize();
        }
    }
}
