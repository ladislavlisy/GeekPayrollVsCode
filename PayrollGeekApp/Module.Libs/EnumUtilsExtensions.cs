using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class EnumUtilsExtensions
    {
        /// <summary>
        /// Gets all items for an enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        public static IEnumerable<T> GetSelectedItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                if ((UInt16)item < 10000)
                {
                    yield return (T)item;
                }
            }
        }
        public static string GetSymbol(this Enum value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Gets all items for an enum type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
        public static IEnumerable<T> GetSelectedItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                if ((UInt16)item < 10000)
                {
                    yield return (T)item;
                }
            }
        }
    }
}
