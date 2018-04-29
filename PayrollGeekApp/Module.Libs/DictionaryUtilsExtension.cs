using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class DictionaryUtilsExtension
    {
        public static IDictionary<TK, IEnumerable<TV>> Merge<TK, TV>(this IDictionary<TK, IEnumerable<TV>> dict, TK key, IEnumerable<TV> value)
        {
            IDictionary<TK, IEnumerable<TV>> freshDict = dict.ToDictionary((kv) => (kv.Key), (kv) => (kv.Value));

            IEnumerable<TV> freshValue = null;

            bool foundKey = freshDict.TryGetValue(key, out freshValue);
            if (foundKey == false)
            {
                freshDict.Add(key, value);
            }
            else
            {
                if (freshValue != null)
                {
                    freshDict[key] = freshValue.Concat(value);
                }
                else
                {
                    freshDict[key] = value;
                }
            }
            return freshDict;
        }
    }
}
