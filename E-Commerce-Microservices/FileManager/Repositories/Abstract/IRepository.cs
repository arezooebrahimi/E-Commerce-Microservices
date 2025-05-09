namespace FileManager.Repositories.Abstract
{
    public interface IRepository<TDocument> where TDocument : class
    {
        Task<List<TDocument>> GetAllAsync();
        Task<TDocument?> GetByIdAsync(string id);
        Task CreateAsync(TDocument document);
        Task<bool> UpdateAsync(string id, TDocument document);
        Task<bool> DeleteAsync(string id);
    }
}
