using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces
{
    using DetailData = IEnumerable<Module.Interfaces.Permadom.ArticleCodeConfigData>;
    using MasterData = IEnumerable<Module.Interfaces.Permadom.ArticleRoleConfigData>;

    using Matrixus;

    public interface IMatrixusService
    {
        void Initialize(MasterData configRoleData, DetailData configCodeDat);
        IArticleConfigProfile Profile();
    }
}
