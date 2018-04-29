using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class ListUtilsExtensions
    {
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> list, IEnumerable<T> value)
        {
            return list.Concat(value.ToList());
        }
    }
}
