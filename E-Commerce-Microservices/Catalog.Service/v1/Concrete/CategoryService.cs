using AutoMapper;
using Catalog.Data.Repositories.EntityFramework.Abstract;
using Catalog.Service.v1.Abstract;
using Common.Dtos.Catalog.Category;


namespace Catalog.Service.v1.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
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
