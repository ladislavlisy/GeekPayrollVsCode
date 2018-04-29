using System;
using System.Linq;

namespace ElementsLib.Service.Matrixus
{
    using Elements;
    using Permadom;
    using ElementsLib.Matrixus.Config;
    using Module.Interfaces.Permadom;

    public class SimpleMatrixusService : MatrixusService
    {
        public SimpleMatrixusService() : base()
        {
            ModuleAssembly = typeof(MatrixusService).Assembly;

            InternalConfigFactory = new ArticleConfigFactory();

            InternalConfigProfile = new ArticleConfigProfile();
        }

        public void InitializeService(IPermadomService configMemoryDb)
        {
            var configCodeData = configMemoryDb.GetArticleCodeData().ToList();

            var configRoleData = configMemoryDb.GetArticleRoleData().ToList();

            Initialize(configRoleData, configCodeData);
        }
    }
}
