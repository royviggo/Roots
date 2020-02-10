using System;
using System.Linq;
using System.Linq.Expressions;

namespace Roots.Business.Helpers
{
    public static class FilterHelper
    {
        //public static IQueryable<T> ContainsO<T>(this IQueryable<T> source, string text, string value)
        //{
        //    var expression = True<T>().Or(t => text.Contains(value)).Or(t => text.Contains(value));
        //    return source.Where(expression);
        //}

        //public static IQueryable<T> ContainsOr<T>(this IQueryable<T> source, Func<T, bool> text1, Func<T, bool> text2, string value)
        //{
        //    var expression = True<T>().Or(t => text1.Contains(value)).Or(t => text1.Contains(value));
        //    return source.Where(expression);
        //}

        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>> (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>> (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

    }
}
