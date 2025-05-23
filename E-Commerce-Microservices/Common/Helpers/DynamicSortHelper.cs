using Common.Dtos.Common;
using System.Linq.Expressions;

namespace Common.Helpers
{
    public static class DynamicSortHelper
    {
         public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortOptions sort, List<string>? icludes)
        {
            if (string.IsNullOrEmpty(sort.Column))
                throw new ArgumentException("Sort column is required.");

            if (icludes != null && icludes.Any())
            {
                foreach (var include in icludes)
                {
                    List<string> parts = include.Split('.').ToList(); //Tags.Tag tagsTag
                    parts[0] = char.ToLowerInvariant(parts[0][0]) + parts[0].Substring(1);
                    var lowerCaseInclude = string.Join("", parts); //parentName //parent

                    if (sort.Column.StartsWith(lowerCaseInclude))
                    {
                        var suffix = sort.Column.Substring(lowerCaseInclude.Length);
                        parts.Add(suffix);
                        sort.Column = string.Join(".", parts);
                        break;
                    }
                }
            }

            var parameter = Expression.Parameter(typeof(T), "p");
            Expression propertyAccess = parameter;

            foreach (var member in sort.Column.Split('.'))
            {
                var property = propertyAccess.Type.GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(member, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                    throw new ArgumentException($"Sort column '{sort.Column}' does not exist on type '{propertyAccess.Type.Name}'.");

                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property); //p => p.Parent

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.PropertyType != typeof(string))
                {
                    var elementType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                    if (elementType == null)
                        throw new ArgumentException($"Cannot sort by collection '{member}'.");

                    propertyAccess = Expression.Call(   //p => p.Categories.FirstOrDefault()
                        typeof(Enumerable),
                        "FirstOrDefault",
                        new Type[] { elementType },
                        propertyAccess
                    );
                }
            }

            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = sort.Order?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            var resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { typeof(T), propertyAccess.Type },
                query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}
