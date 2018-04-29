using System;
using System.Collections.Generic;

namespace ElementsLib.Module.Interfaces.Matrixus
{
    using Elements;
    using ResultMonad;

    public interface IConfigCollection<TConfig, TIndex>
    {
        TConfig FindArticleConfig(TIndex modelCode);
    }
}