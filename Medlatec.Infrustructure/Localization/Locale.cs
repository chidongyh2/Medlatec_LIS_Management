using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Medlatec.Infrastructure.Localization
{
    public enum Locale
    {
        English = 1,
        Vietnamese,
    }

    public static class CultureName
    {
        public const string Vietnamese = "vi-VN";
        public const string English = "en-US";

        public static string Default = Vietnamese;
    }

    public static class CultureHelper
    {
        private static IDictionary<string, Locale> mapper = new Dictionary<string, Locale>()
        {
            {CultureName.Vietnamese, Locale.Vietnamese},
            {CultureName.English, Locale.English},
        };

        public static Locale ToLocale(this CultureInfo @this)
        {
            return mapper.TryGetValue(@this.Name, out var locale) ? locale : Locale.Vietnamese;
        }

        public static Locale? ToLocale(this string name)
        {
            if (mapper.TryGetValue(name, out var locale))
            {
                return locale;
            };
            return null;
        }
    }
}
