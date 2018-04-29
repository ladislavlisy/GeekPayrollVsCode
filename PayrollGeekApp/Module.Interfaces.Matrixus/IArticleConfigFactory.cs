using System;
using System.Reflection;
using System.Collections.Generic;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using MasterCode = UInt16;
    using MasterName = String;
    using MasterItem = Matrixus.IArticleConfigMaster;
    using MasterStub = Elements.IArticleSource;

    using DetailCode = UInt16;
    using DetailType = UInt16;
    using DetailBind = UInt16;
    using DetailName = String;
    using DetailItem = Matrixus.IArticleConfigDetail;


    public interface IArticleConfigFactory
    {
        MasterStub CreateSourceClassStub(Assembly configAssembly, MasterCode symbolCode, MasterName symbolName);
        MasterItem CreateMasterItem(Assembly configAssembly, MasterCode symbolCode, MasterName symbolName, params MasterCode[] symbolPath);
        DetailItem CreateDetailItem(IArticleMasterCollection masterStore, DetailCode symbolCode, DetailName symbolName, MasterCode symbolRole, DetailType symbolType, DetailBind symbolBind, params DetailCode[] symbolPath);
    }
}
