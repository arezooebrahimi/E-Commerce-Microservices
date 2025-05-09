
using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Dtos.Common;
using Common.Entities;

namespace Admin.Services.Concrete
{
    public class CategoryService: ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResponse<GetCategoriesPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            var (categories, total) = await _categoryRepository.GetAllPaginateAsync(request);
            var response = new PagedResponse<GetCategoriesPaginateDto>()
            {
                Total = total,
                Items = _mapper.Map<List<GetCategoriesPaginateDto>>(categories)
            };

            return response;
        }
    }
}
