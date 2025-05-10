using Common.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.Helpers
{
    public static class DynamicIncludeHelper
    {
        public static IQueryable<T> ApplyIncludes<T>(IQueryable<T> query, Dictionary<string, FilterOptions> filters) where T : class
        {
            var includePaths = filters.Keys
                .Select(key => GetIncludePath(key))
                .Where(path => !string.IsNullOrEmpty(path))
                .Distinct();

            foreach (var path in includePaths)
            {
                var correctedPath = CorrectNavigationPath<T>(path!);
                if (!string.IsNullOrEmpty(correctedPath))
                {
                    query = query.Include(correctedPath);
                }
            }

            return query;
        }

        private static string? GetIncludePath(string propertyPath)
        {
            var parts = propertyPath.Split('.');
            if (parts.Length > 1)
            {
                return string.Join(".", parts.Take(parts.Length - 1));
            }
            return null;
        }

        private static string? CorrectNavigationPath<T>(string path)
        {
            var type = typeof(T);
            var correctedParts = new List<string>();

            foreach (var part in path.Split('.'))
            {
                var propName = Capitalize(part);

                var prop = type.GetProperty(propName);
                if (prop == null)
                {
                    return null;
                }

                correctedParts.Add(propName);

                type = prop.PropertyType;
                if (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                {
                    type = type.GetGenericArguments().First();
                }
            }

            return string.Join(".", correctedParts);
        }

        private static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
