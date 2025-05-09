using Common.Dtos.Common;
using System.Linq.Expressions;
using System.Reflection;


namespace Common.Helpers
{
    public static class DynamicFilterHelper
    {
        public static IQueryable<T> ApplyDynamicFilters<T>(IQueryable<T> query, Dictionary<string, FilterOptions> filters)
        {
            foreach (var filter in filters)
            {
                query = ApplyFilter(query, filter.Key, filter.Value);
            }
            return query;
        }

        private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string propertyPath, FilterOptions filterOptions)
        {
            var param = Expression.Parameter(typeof(T), "p");  //p => p.Property == 5
            Expression predicate = BuildFilterExpression(param, propertyPath.Split('.'), filterOptions);

            var lambda = Expression.Lambda<Func<T, bool>>(predicate, param);
            return query.Where(lambda);
        }

        private static Expression BuildFilterExpression(Expression param, string[] properties, FilterOptions filterOptions, int index = 0)
        {
            var currentProp = properties[index];

            var propertyInfo = param.Type.GetProperty(
                currentProp,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );

            if (propertyInfo == null)
                throw new InvalidOperationException($"Property '{currentProp}' not found on type '{param.Type.Name}'");

            var propertyExpr = Expression.Property(param, propertyInfo);

            bool isCollection = typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)
                                && propertyInfo.PropertyType != typeof(string);

            // If it's a collection (e.g., Categories)
            if (isCollection)
            {
                // Get element type (e.g., Category)
                var elementType = propertyInfo.PropertyType.IsGenericType
                    ? propertyInfo.PropertyType.GetGenericArguments()[0]
                    : propertyInfo.PropertyType.GetElementType();


                if (elementType == null)
                    throw new InvalidOperationException($"Collection property '{currentProp}' does not have a valid element type.");

                var innerParam = Expression.Parameter(elementType, "x");
                var innerExpr = BuildFilterExpression(innerParam, properties, filterOptions, index + 1);
                var anyLambda = Expression.Lambda(innerExpr, innerParam); //c => c.Name == "Test"

                var anyMethod = typeof(Enumerable).GetMethods()  //Product.Categories.Any(c => c.Name == "Test")
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(elementType);

                return Expression.Call(anyMethod, propertyExpr, anyLambda); //p.Categories.Any(x => x.Name == "Test")
            }
            else
            {
                if (index == properties.Length - 1)
                {
                    Expression? finalExpr = null;

                    foreach (var mode in filterOptions.FilterModes)
                    {
                        var comparisonExpr = BuildComparisonExpression(propertyExpr, mode);

                        finalExpr = finalExpr == null
                            ? comparisonExpr
                            : (filterOptions.Operator.ToLower() == "or"
                                ? Expression.OrElse(finalExpr, comparisonExpr)
                                : Expression.AndAlso(finalExpr, comparisonExpr));
                    }

                    return finalExpr!;
                }

                return BuildFilterExpression(propertyExpr, properties, filterOptions, index + 1);
            }
        }

        private static Expression BuildComparisonExpression(Expression propertyExpr, FilterMode mode)
        {
            var targetType = propertyExpr.Type;
            var constant = GetTypedConstant(targetType, mode.Value.ToString()!);

            var comparisonMethods = new Dictionary<string, Func<Expression, Expression, Expression>>()
            {
                { "equals", (prop, constExpr) => Expression.Equal(prop, constExpr) },
                { "notEquals", (prop, constExpr) => Expression.NotEqual(prop, constExpr) },
                { "contains", (prop, constExpr) => Expression.Call(prop, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constExpr) },
                { "notContain", (prop, constExpr) => Expression.Not(Expression.Call(prop, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constExpr)) },
                { "startsWith", (prop, constExpr) => Expression.Call(prop, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, constExpr) },
                { "endsWith", (prop, constExpr) => Expression.Call(prop, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, constExpr) },
                { "greaterThan", (prop, constExpr) => Expression.GreaterThan(prop, constExpr) },
                { "lessThan", (prop, constExpr) => Expression.LessThan(prop, constExpr) },
                { "greaterThanOrEqual", (prop, constExpr) => Expression.GreaterThanOrEqual(prop, constExpr) },
                { "lessThanOrEqual", (prop, constExpr) => Expression.LessThanOrEqual(prop, constExpr) }
            };

            if (!comparisonMethods.ContainsKey(mode.Mode))
                throw new NotSupportedException($"Filter mode '{mode.Mode}' is not supported");

            return comparisonMethods[mode.Mode](propertyExpr, constant);
        }


        private static ConstantExpression GetTypedConstant(Type targetType, string value)
        {
            if (targetType == typeof(Guid) || targetType == typeof(Guid?))
                return Expression.Constant(Guid.Parse(value), targetType);

            if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                return Expression.Constant(DateTime.Parse(value), targetType);

            if (targetType.IsEnum || (Nullable.GetUnderlyingType(targetType)?.IsEnum ?? false))
                return Expression.Constant(Enum.Parse(Nullable.GetUnderlyingType(targetType) ?? targetType, value), targetType);

            var converted = Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType) ?? targetType);
            return Expression.Constant(converted, targetType);
        }

        private static Type GetUnderlyingType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
