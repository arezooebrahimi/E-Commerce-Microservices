using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Helpers
{
    public static class DynamicIncludeHelper
    {
        public static IQueryable<T> ApplyIncludes<T>(IQueryable<T> query, IEnumerable<string> includePaths) where T : class
        {
            foreach (var path in includePaths.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                query = query.Include(BuildIncludeExpression<T>(path));
            }

            return query;
        }

        private static Expression<Func<T, object>> BuildIncludeExpression<T>(string propertyPath)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression body = parameter;

            foreach (var member in propertyPath.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            // Convert to object
            if (body.Type.IsValueType)
                body = Expression.Convert(body, typeof(object));

            return Expression.Lambda<Func<T, object>>(body, parameter);
        }
    }
}
