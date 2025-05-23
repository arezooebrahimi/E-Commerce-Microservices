﻿
using Admin.Dtos.Common;
using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using Admin.Services.Grpc;
using AutoMapper;
using Common.Dtos.Admin.Category;
using Common.Dtos.Catalog.Category;
using Common.Dtos.Common;
using Common.Entities;
using Common.Exceptions;

namespace Admin.Services.Concrete
{
    public class CategoryService: ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly FileManagerGrpcClient _grpcClient;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper, FileManagerGrpcClient grpcClient)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _grpcClient = grpcClient;
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
            bool isDuplicate = await _categoryRepository.IsSlugDuplicateAsync(request.Slug);
            if (isDuplicate)
                throw new AppException($"Slug '{request.Slug}' is already in use.");

            var newEntity = _mapper.Map<Category>(request);
            if (request.ImageOnHomePage != null)
            {
                var mediaIds = await _grpcClient.UploadFilesAsync([request.ImageOnHomePage]);
                if (mediaIds.Count() != 0)
                    newEntity.ImageIdOnHomePage = mediaIds[0];
            }

            if (request.Medias != null)
            {
                var mediaIds = await _grpcClient.UploadFilesAsync(request.Medias);
                newEntity.Medias = new List<CategoryMedia>();
                foreach (var item in mediaIds)
                {
                    newEntity.Medias.Add(new CategoryMedia
                    {
                        MediaId = item,
                        IsPrimary = true
                    });
                }
            }

            var entity = await _categoryRepository.AddAsync(newEntity);
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
