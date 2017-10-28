using System;
using System.Linq.Expressions;
using System.Reflection;

namespace jdx.ApplManga.Core.Expressions {

    public static class ExpressionHelpers {

        /// <summary>
        /// Compiles an expression and gets the function's return value
        /// </summary>
        /// <typeparam name="T">Return value type</typeparam>
        /// <param name="lambda">Expression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda) {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Sets the underlying property's value to the given expression's value
        /// </summary>
        /// <typeparam name="T">Return value to set</typeparam>
        /// <param name="lambda">Expression to compile</param>
        /// <param name="value">Value to set the property to</param>
        /// <returns></returns>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value) {
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            // Set the property value
            propertyInfo.SetValue(target, value);
        }
    }
}
