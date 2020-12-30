using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Medlatec.Infrastructure.Helpers
{
    public static class ClassHelper
    {
        public static string NameSpace<T>() where T : class
        {
            return typeof(T).Namespace;
        }

        public static string ClassName<T>() where T : class
        {
            return typeof(T).Name;
        }

        public static List<string> GetPropertiesName<T>() where T : class
        {
            return typeof(T).GetProperties().Select(x => x.Name).ToList();
        }

        public static List<string> GetPropertiesNameAsKey<T>() where T : class
        {
            var nameSpace = NameSpace<T>();
            var className = ClassName<T>();
            return typeof(T).GetProperties().Select(x => $"{nameSpace}.{className}.{x.Name}").ToList();
        }

        public static string GetPropertyNameAsKey<T>(string propertyName) where T : class
        {
            var nameSpace = NameSpace<T>();
            var className = ClassName<T>();
            return $"{nameSpace}.{className}.{propertyName}";
        }

        public static string GetDisplayName<T>(string propertyName)
        {
            var properties = typeof(T).GetProperties();
            var propertyInfo = properties.FirstOrDefault(x => x.Name == propertyName);
            if (propertyInfo == null) return null;

            var attributes = propertyInfo.GetCustomAttributes(true);
            if (attributes.FirstOrDefault() is DisplayNameAttribute displayNameAttribute) return displayNameAttribute.DisplayName;
            return null;
        }
    }
}
