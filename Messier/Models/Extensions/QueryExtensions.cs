using System;
using System.Linq;
using System.Linq.Expressions;

namespace DST.Models.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query
                ?.Skip((pageNumber - 1) * pageSize)
                ?.Take(pageSize);
        }

        public static IOrderedQueryable<T> AppendOrderBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
        {
            return query.Expression.Type == typeof(IOrderedQueryable<T>)
                ? ((IOrderedQueryable<T>)query).ThenBy(keySelector)
                : query.OrderBy(keySelector);
        }

        public static IOrderedQueryable<T> AppendOrderByDescending<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
        {
            return query.Expression.Type == typeof(IOrderedQueryable<T>)
                ? ((IOrderedQueryable<T>)query).ThenByDescending(keySelector)
                : query.OrderByDescending(keySelector);
        }
    }
}
