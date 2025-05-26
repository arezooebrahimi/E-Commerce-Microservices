using Common.Dtos.Common;
using Common.Dtos.FileManager;
using Common.Entities.Abstract;
using FileManager.Helpers;
using FileManager.Models.Mongodb;
using FileManager.Repositories.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileManager.Repositories.Concrete
{
    public class Repository<TDocument> : IRepository<TDocument> where TDocument : class, ICreatedAtEntity,IFilePathEntity
    {
        private readonly IMongoCollection<TDocument> _collection;

        public Repository(IOptions<MongoDbSettings> mongoSettings, string collectionName)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _collection = database.GetCollection<TDocument>(collectionName);
            MongoIndexBuilder.ApplyIndexes(_collection);
        }

        public async Task<PagedResponse<TDocument>> GetAllAsync(GetMediasRequest req)
        {
            var response = new PagedResponse<TDocument>();
            var filter = Builders<TDocument>.Filter.Empty;
            if (!string.IsNullOrEmpty(req.Filter))
            {
                var fileNameFilter = Builders<TDocument>.Filter.Regex("FileName", new BsonRegularExpression(req.Filter, "i"));
                filter = fileNameFilter;
            }

            var total = await _collection.CountDocumentsAsync(filter);

            response.Items = await _collection
              .Find(filter)
              .Sort(Builders<TDocument>.Sort.Descending(x => x.CreatedAt))
              .Skip(req.Offset)
              .Limit(req.Limit)
              .ToListAsync();

            response.Total = total;
            return response;
        }

        public async Task<bool> ExistsByFilePathAsync(string filePath)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.FilePath, filePath);
            var count = await _collection.CountDocumentsAsync(filter);
            return count > 0;
        }


        public async Task<TDocument?> GetByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task<bool> UpdateAsync(string id, TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.ReplaceOneAsync(filter, document);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(List<string> ids)
        {
            var objectIds = ids.Select(ObjectId.Parse).ToList();
            var filter = Builders<TDocument>.Filter.In("_id", objectIds);
            var result = await _collection.DeleteManyAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
