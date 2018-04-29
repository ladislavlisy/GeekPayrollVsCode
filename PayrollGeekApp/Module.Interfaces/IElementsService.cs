using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces
{
    using Elements;
    public interface IElementsService
    {
        IArticleSourceStore SourceStream();
    }
}
