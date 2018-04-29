using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Libs
{
    public static class StringUtilsExtension
    {
        public static string Camelize(this string value, bool firstLetterUppercase = true)
        {
            if (firstLetterUppercase)
            {
                return
                    Regex.Replace(
                        Regex.Replace(value, "/(.?)", p => "::" + p.Groups[1].Value.ToUpperInvariant()),
                        "(?:^|_)(.)", p => p.Groups[1].Value.ToUpperInvariant()
                    );
            }
            else
            {
                return
                    value.Substring(0, 1).ToLowerInvariant() +
                    Camelize(value.Substring(1));
            }
        }

        public static string Underscore(this string value)
        {
            value = value.Replace("::", "/");
            value = Regex.Replace(value, "([A-Z]+)([A-Z][a-z])", p => p.Groups[1].Value + "_" + p.Groups[2].Value);
            value = Regex.Replace(value, "([a-z\\d])([A-Z])", p => p.Groups[1].Value + "_" + p.Groups[2].Value);
            value = value.Replace("-", "_");

            return value.ToLowerInvariant();
        }
    }
    public static class StringFormatExtension
    {
        public static string FormatAmount(this string value)
        {
            // .gsub(/(\d)(?=(\d\d\d)+(?!\d))/, "\\1 ")
            return Regex.Replace(value, "(\\d)(?=(\\d\\d\\d)+(?!\\d))", p => p.Groups[1].Value + " ",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
    public static class StringEnumExtension
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IComparable
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
        public static T ToEnum<T>(this string value) where T : struct, IComparable
        {
            T defaultValue = (T)Enum.GetValues(typeof(T)).GetValue(0);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
    }
    public static class UInt16EnumExtension
    {
        public static T ToEnum<T>(this UInt16 value, T defaultValue) where T : struct, IComparable
        {
            if (Enum.IsDefined(typeof(T), value)==false)
            {
                return defaultValue;
            }

            return (T)Enum.ToObject(typeof(T), value);
        }
        public static T ToEnum<T>(this UInt16 value) where T : struct, IComparable
        {
            T defaultValue = (T)Enum.GetValues(typeof(T)).GetValue(0);

            if (Enum.IsDefined(typeof(T), value)==false)
            {
                return defaultValue;
            }

            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
