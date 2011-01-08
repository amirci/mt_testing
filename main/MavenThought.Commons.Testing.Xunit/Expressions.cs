using System;
using System.Linq.Expressions;
using Xunit;

namespace MavenThought.Commons.Testing
{
    internal static class Expressions
    {
        /// <summary>
        /// Returns the name of the property
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="property">Expression to get the property name</param>
        /// <returns>The name of the property</returns>
        public static string GetPropertyName<T>(this Expression<Func<T>> property)
        {
            var memberExpr = property.Body as MemberExpression;
            var methodExpr = property.Body as MethodCallExpression;

            Assert.True(memberExpr != null || methodExpr != null);

            return memberExpr == null ? methodExpr.Method.Name : memberExpr.Member.Name;
        }

        /// <summary>
        /// Returns the name of the property or method
        /// </summary>
        /// <typeparam name="TSource">Type of the source of the property</typeparam>
        /// <typeparam name="TResult">Type of the result of the property</typeparam>
        /// <param name="property">Expression to get the property name</param>
        /// <returns>The name of the property</returns>
        public static string GetPropertyName<TSource, TResult>(this Expression<Func<TSource, TResult>> property)
        {
            var memberExpr = property.Body as MemberExpression;
            var methodExpr = property.Body as MethodCallExpression;
            
            Assert.True(memberExpr != null || methodExpr != null);

            return memberExpr == null ? methodExpr.Method.Name : memberExpr.Member.Name;
        }

        /// <summary>
        /// Returns the name of the method
        /// </summary>
        /// <typeparam name="TSource">Type of the source of the property</typeparam>
        /// <param name="property">Expression to get the property name</param>
        /// <returns>The name of the property</returns>
        public static string GetPropertyName<TSource>(this Expression<Action<TSource>> property)
        {
            var methodExpr = (MethodCallExpression) property.Body;

            return methodExpr.Method.Name;
        }
    }
}