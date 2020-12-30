
using FluentValidation;
using Medlatec.Infrastructure.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using Medlatec.Infrastructure.Extensions;
namespace Medlatec.Infrastructure.Helpers
{
    public static class CustomValidators
    {
        private static IStringLocalizerFactory StringLocalizerFactory { set; get; }

        /// <summary>
        /// Validate phone number.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TElement> MustBePhoneNumber<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, string message)
        {
            return ruleBuilder.Must((rootObject, element, context) =>
            {
                if (element == null)
                    return false;

                var elementValue = element.ToString();
                return elementValue.IsPhoneNumber();
            })
            .WithMessage(message);
        }

        /// <summary>
        /// Validate number.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TElement> MustBeNumber<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, string message)
        {
            //return ruleBuilder
            //    .Must(x => decimal.TryParse(x.ToString(), out decimal value))
            //    .When(x => x != null)
            //    .WithLocalizedMessage(typeof(SharedResource), "{PropertyName} must be a number.");

            return ruleBuilder.Must((rootObject, element, context) =>
            {
                if (element == null)
                    return false;

                var elementValue = element.ToString();
                return int.TryParse(elementValue, out int number) || decimal.TryParse(elementValue,
                                                                      out decimal decimalNumber)
                                                                  || float.TryParse(elementValue,
                                                                      out float floatNumber) ||
                                                                  double.TryParse(elementValue,
                                                                      out double doubleNumber);
            })
                .WithMessage(message);
        }

        /// <summary>
        /// Validate email.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TElement> MustBeEmail<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, string message)
        {
            //return ruleBuilder
            //    .Must(x => x.ToString().IsPhoneNumber())
            //    .When(x => x != null)
            //    .WithLocalizedMessage(typeof(SharedResource), "Invalid email");
            return ruleBuilder.Must((rootObject, element, context) =>
            {
                var elementValue = element.ToString();
                return string.IsNullOrEmpty(elementValue) || elementValue.IsEmailAddress();
            })
                .WithMessage(message);
        }

        /// <summary>
        /// Validate range of list object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainGreaterThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder,
            int num)
        {
            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter.AppendArgument("MinElements", num);
                return list.Count > num;
            })
            .WithMessage("{PropertyName} must contain greater than {MinElements} items.");
        }

        public static IRuleBuilderOptions<T, TElement> NotNullAndEmpty<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, string message)
        {
            //return ruleBuilder
            //    .Must(x => x.ToString().IsPhoneNumber())
            //    .When(x => x != null)
            //    .WithLocalizedMessage(typeof(SharedResource), "Invalid email");
            return ruleBuilder.Must((rootObject, element, context) =>
            {
                if (element == null)
                    return false;

                var elementValue = element.ToString()?.Trim();
                return !string.IsNullOrEmpty(elementValue);
            })
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<T, TElement> MustBeValidDate<T, TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, string message)
        {
            return ruleBuilder.Must((rootObject, element, context) =>
            {
                if (element == null)
                    return true;

                var elementValue = element.ToString();
                if (string.IsNullOrEmpty(elementValue))
                    return true;

                var isValid = DateTime.TryParse(elementValue, out var date);
                return isValid;
            })
                .WithMessage(message);
        }
    }
}
