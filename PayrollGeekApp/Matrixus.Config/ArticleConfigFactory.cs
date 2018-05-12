using System;

namespace ElementsLib.Matrixus.Config
{
    using MasterCode = UInt16;
    using MasterName = String;
    using MasterItem = Module.Interfaces.Matrixus.IArticleConfigMaster;
    using MasterData = Module.Interfaces.Permadom.ArticleRoleConfigData;
    using MasterStub = Module.Interfaces.Elements.IArticleSource;

    using DetailCode = UInt16;
    using DetailGang = UInt16;
    using DetailType = UInt16;
    using DetailBind = UInt16;
    using DetailName = String;
    using DetailItem = Module.Interfaces.Matrixus.IArticleConfigDetail;
    using DetailData = Module.Interfaces.Permadom.ArticleCodeConfigData;

    using Module.Interfaces.Matrixus;
    using System.Reflection;
    using Module.Common;
    using Legalist.Constants;

    public class ArticleConfigFactory : IArticleConfigFactory
    {
        private const string ARTICLE_CLASS_POSTFIX = "Article";
        private const string ARTICLE_CLASS_PATTERN = "ARTICLE_(.*)";
        private const string ARTICLE_SPACE_PATTERN = "ElementsLib.Elements.Config.Articles";
        private const string ARTICLE_CLASS_DEFAULT = "ARTICLE_UNKNOWN";

        public MasterItem CreateMasterItem(Assembly configAssembly, MasterCode symbolCode, MasterName symbolName, params MasterCode[] symbolPath)
        {
            MasterStub elementStub = CreateSourceClassStub(configAssembly, symbolCode, symbolName);

            MasterItem elementItem = new ArticleConfigMaster(symbolCode, symbolName, elementStub, symbolPath);

            return elementItem;
        }

        public MasterStub CreateSourceClassStub(Assembly configAssembly, MasterCode symbolCode, MasterName symbolName)
        {
            string symbolClass = CreateSourceClassName(symbolName);

            string backupClass = CreateSourceClassName(ARTICLE_CLASS_DEFAULT);

            MasterStub elementStub = GeneralClazzFactory<MasterStub>.InstanceFor(configAssembly, ARTICLE_SPACE_PATTERN, symbolClass, backupClass);

            return elementStub;
        }
        protected MasterName CreateSourceClassName(MasterName symbolName)
        {
            string symbolClass = GeneralNamesFactory.ClassNameFor(ARTICLE_CLASS_POSTFIX, ARTICLE_CLASS_PATTERN, symbolName);

            return symbolClass;
        }
        public DetailItem CreateDetailItem(IArticleMasterCollection masterStore, DetailCode symbolCode, 
            DetailName symbolName, MasterCode symbolRole, DetailGang symbolGang, 
            DetailType symbolType, DetailBind symbolBind, 
            TaxingBehaviour taxingType, HealthBehaviour healthType, SocialBehaviour socialType, 
            params DetailCode[] symbolPath)
        {
            MasterItem elementNode = masterStore.FindArticleConfig(symbolRole);

            DetailItem elementItem = new ArticleConfigDetail(symbolCode, symbolName, 
                symbolGang, symbolType, symbolBind, 
                taxingType, healthType, socialType, 
                symbolPath);

            MasterStub elementStub = elementNode.CloneMasterStub(
                symbolCode, symbolRole, symbolGang, 
                symbolType, symbolBind, 
                taxingType, healthType, socialType);

            elementItem.SetSymbolRole(symbolRole, elementStub);

            return elementItem;
        }
    }
}
