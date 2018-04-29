using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Permadom
{
    using ArticleCodeConfigItem = Module.Interfaces.Permadom.ArticleCodeConfigData;
    using ArticleRoleConfigItem = Module.Interfaces.Permadom.ArticleRoleConfigData;

    public interface IPermadomService
    {
        IEnumerable<ArticleCodeConfigItem> GetArticleCodeData();
        IEnumerable<ArticleRoleConfigItem> GetArticleRoleData();
    }
}
