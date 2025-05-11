
using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Category;
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
            request.Includes = new List<string>() { "Parent" };
            var (categories, total) = await _categoryRepository.GetAllPaginateAsync(request);
            var response = new PagedResponse<GetCategoriesPaginateDto>()
            {
                Total = total,
                Items = _mapper.Map<List<GetCategoriesPaginateDto>>(categories)
            };

            return response;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }


        public async Task<IEnumerable<Category>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _categoryRepository.GetAllByIdsAsync(ids);
        }

        public async Task<Category> AddAsync(CreateCategoryRequest request)
        {
            var entity = await _categoryRepository.AddAsync(_mapper.Map<Category>(request));
            await _categoryRepository.SaveChangesAsync();
            return entity;
        }


        public async Task<Category?> UpdateAsync(CreateCategoryRequest request)
        {
            Category? entity = null;
            if (request.Id != null)
            {
                entity = await _categoryRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _categoryRepository.SaveChangesAsync();
                }
            }
            return entity;
        }


        public async Task DeleteAsync(IEnumerable<Category> request)
        {
            _categoryRepository.DeleteRange(request);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
