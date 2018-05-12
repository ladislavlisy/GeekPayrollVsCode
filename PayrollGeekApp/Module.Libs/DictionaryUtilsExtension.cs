using ElementsLib.Matrixus.Config;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static IDictionary<TK, KeyValuePair<TG, IEnumerable<TV>>> Merge<TK, TG, TV>(this IDictionary<TK, KeyValuePair<TG, IEnumerable<TV>>> dict, TK key, TG group, IEnumerable<TV> value)
        {
            IDictionary<TK, KeyValuePair<TG, IEnumerable<TV>>> freshDict = dict.ToDictionary((kv) => (kv.Key), (kv) => (kv.Value));

            KeyValuePair<TG, IEnumerable<TV>> freshValue;

            bool foundKey = freshDict.TryGetValue(key, out freshValue);
            if (foundKey == false)
            {
                freshDict.Add(key, new KeyValuePair<TG, IEnumerable<TV>>(group, value));
            }
            else
            {
                if (freshValue.Value != null)
                {
                    freshDict[key] = new KeyValuePair<TG, IEnumerable<TV>>(group, freshValue.Value.Concat(value));
                }
                else
                {
                    freshDict[key] = new KeyValuePair<TG, IEnumerable<TV>>(group, value);
                }
            }
            return freshDict;
        }
        public static IDictionary<TK, ArticleReferenceSort<TG, TV>> Merge<TK, TG, TV>(this IDictionary<TK, ArticleReferenceSort<TG, TV>> dict, TK key, TG group, IEnumerable<TV> value)
        {
            IDictionary<TK, ArticleReferenceSort<TG, TV>> freshDict = dict.ToDictionary((kv) => (kv.Key), (kv) => (kv.Value));

            ArticleReferenceSort<TG, TV> freshValue;

            bool foundKey = freshDict.TryGetValue(key, out freshValue);
            if (foundKey == false)
            {
                freshDict.Add(key, new ArticleReferenceSort<TG, TV>(group, value));
            }
            else
            {
                if (freshValue != null)
                {
                    freshDict[key] = new ArticleReferenceSort<TG, TV>(group, freshValue.Path().Concat(value));
                }
                else
                {
                    freshDict[key] = new ArticleReferenceSort<TG, TV>(group, value);
                }
            }
            return freshDict;
        }
    }
}
