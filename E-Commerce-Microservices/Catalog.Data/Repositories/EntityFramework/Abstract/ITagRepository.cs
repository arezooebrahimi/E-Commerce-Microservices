using Common.Entities;

namespace Catalog.Data.Repositories.EntityFramework.Abstract
{
    public interface ITagRepository
    {
        Task<Tag?> GetTagBySlug(string slug);
    }
}
