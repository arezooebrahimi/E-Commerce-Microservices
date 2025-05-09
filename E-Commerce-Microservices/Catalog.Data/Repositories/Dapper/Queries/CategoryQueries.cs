using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories.Dapper.Queries
{
    public static class CategoryQueries
    {
        public const string GetHomePageCategories = @"
        SELECT 
            ""Name"",
            ""Slug""
        FROM public.""Categories""
        WHERE ""DisplayOnHomePage"" = true";
    }
}
