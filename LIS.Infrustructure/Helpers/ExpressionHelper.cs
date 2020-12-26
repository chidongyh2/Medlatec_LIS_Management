using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace LIS.Infrastructure.Helpers
{
    /// <summary>
    /// Extension for predicates expressions (composing, anding, oring...)
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Composes the two expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="merge">The merge.</param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this LambdaExpression first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// Predicate 1  AND predicate 2.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        /// <summary>
        /// Predicate 1 OR Predicate 2.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        /// <summary>
        /// Run the predicate for the given item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spec">The spec.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool Match<T>(this Expression<Func<T, bool>> spec, T target) where T : class
        {
            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            return spec.Compile().Invoke(target);
        }

        public static string StripTagsRegex(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
