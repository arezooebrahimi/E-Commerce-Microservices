using Common.Dtos.Common;
using System.Linq.Expressions;

namespace Common.Helpers
{
    public static class DynamicSortHelper
    {
        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortOptions sort)
        {
            if (string.IsNullOrEmpty(sort.Column))
                throw new ArgumentException("Sort column is required.");

            var property = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.Equals(sort.Column, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                throw new ArgumentException($"Sort column '{sort.Column}' does not exist on type '{typeof(T).Name}'.");

            var parameter = Expression.Parameter(typeof(T), "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter); // p => p.Name

            string methodName = sort.Order?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            var resultExp = Expression.Call(typeof(Queryable), methodName,  //query.OrderBy(p => p.Price)
                new Type[] { typeof(T), property.PropertyType },
                query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}
