using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories.EntityFramework.Abstract
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetHomePageCategoriesAsync();
    }
}
