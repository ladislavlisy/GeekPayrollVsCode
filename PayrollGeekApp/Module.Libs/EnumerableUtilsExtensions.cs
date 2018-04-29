using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class EnumerableUtilsExtensions
    {
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> collection, T value)
        {
            return collection.Concat(new T[] { value });
        }
    }
}
