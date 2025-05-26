using Common.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;

namespace FileManager.Helpers
{
    public static class MongoIndexBuilder
    {
        public static void ApplyIndexes<T>(IMongoCollection<T> collection)
        {
            var indexModels = new List<CreateIndexModel<T>>();

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<MongoIndexAttribute>();
                if (attr != null)
                {
                    var fieldName = prop.Name;
                    var expr = BuildExpression<T>(fieldName);

                    var keys = attr.Ascending
                        ? Builders<T>.IndexKeys.Ascending(expr)
                        : Builders<T>.IndexKeys.Descending(expr);

                    var indexModel = new CreateIndexModel<T>(keys, new CreateIndexOptions
                    {
                        Name = attr.Name ?? $"idx_{fieldName.ToLower()}",
                        Unique = attr.Unique
                    });

                    indexModels.Add(indexModel);
                }
            }

            if (indexModels.Count > 0)
            {
                collection.Indexes.CreateMany(indexModels);
            }
        }

        private static Expression<Func<T, object>> BuildExpression<T>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.Convert(Expression.PropertyOrField(param, propertyName), typeof(object));
            return Expression.Lambda<Func<T, object>>(body, param);
        }
    }
}
