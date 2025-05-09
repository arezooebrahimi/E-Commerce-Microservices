using FileManager.Models.Mongodb;
using FileManager.Repositories.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileManager.Repositories.Concrete
{
    public class Repository<TDocument> : IRepository<TDocument> where TDocument : class
    {
        private readonly IMongoCollection<TDocument> _collection;

        public Repository(IOptions<MongoDbSettings> mongoSettings, string collectionName)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _collection = database.GetCollection<TDocument>(collectionName);
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            var result = await _collection.FindAsync(_ => true);
            return await result.ToListAsync();
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

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
