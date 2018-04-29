using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class CloneUtils<T> where T : class, ICloneable
    {
        public static T CloneOrNull(T cloneable)
        {
            if (cloneable == null)
            {
                return null;
            }
            return (T)cloneable.Clone();
        }
    }
}
