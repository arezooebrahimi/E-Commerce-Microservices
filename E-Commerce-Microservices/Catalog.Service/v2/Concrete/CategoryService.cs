using AutoMapper;
using Catalog.Data.Repositories.Dapper.Abstract;
using Catalog.Service.v2.Abstract;
using Common.Dtos.Catalog.Category;

namespace Catalog.Service.v2.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDapperRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryDapperRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryHomePageResponse>> GetHomePageCategoriesAsync()
        {
            var categories = await _categoryRepository.GetHomePageCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryHomePageResponse>>(categories);
        }
    }
}
