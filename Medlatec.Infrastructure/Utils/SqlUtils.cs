using System.Text;
using System.Text.RegularExpressions;

namespace Medlatec.Infrastructure.Utils
{
    public static class SqlUtils
    {
        public static string Order(string alias, string fallback, params string[] sorts)
        {

            if (sorts == null || sorts.Length == 0)
            {
                return string.Empty;
            }

            var orderSql = new StringBuilder();
            for (var i = 0; i < sorts.Length; i++)
            {
                var fieldSort = sorts[i];
                if (fieldSort == null)
                {
                    continue;
                }
                fieldSort = Regex.Replace(fieldSort, @"-|\+", "");
                orderSql.Append(string.IsNullOrEmpty(alias)
                    ? $"\"{fieldSort}\""
                    : $" {alias}.\"{Regex.Replace(fieldSort, @"-|\+", "")}\"");
                orderSql.Append(sorts[i].StartsWith("-") ? " DESC" : " ASC");
                if (i < sorts.Length - 1)
                {
                    orderSql.Append(",");
                }

            }

            var sql = orderSql.ToString();
            if (string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(fallback))
            {
                return Order(alias, null, fallback);
            }
            return sql;
        }
    }
}
