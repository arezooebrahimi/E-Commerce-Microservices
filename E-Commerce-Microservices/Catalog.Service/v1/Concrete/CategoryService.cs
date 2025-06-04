using AutoMapper;
using Catalog.Data.Repositories.EntityFramework.Abstract;
using Catalog.Service.v1.Abstract;
using Catalog.Service.v1.Grpc;
using Common.Dtos.Catalog.Category;
using Common.Entities;


namespace Catalog.Service.v1.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly GetFilesGrpcClient _filesClient;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, GetFilesGrpcClient filesClient) 
        { 
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _filesClient = filesClient;
        }

        public async Task<IEnumerable<CategoryHomePageResponse>> GetHomePageCategoriesAsync()
        {
            var categories = await _categoryRepository.GetHomePageCategoriesAsync();
            var entities = _mapper.Map<IEnumerable<CategoryHomePageResponse>>(categories);
            var mediaIds = new List<string>();
            string? mediaId = null;
            CategoryMedia? categoryMedia = null;

            foreach (var item in entities)
            {
                mediaId = item.Medias?.Where(m => !m.IsPrimary).Select(m => m.MediaId).FirstOrDefault();
                if (mediaId != null)
                    mediaIds.Add(mediaId);
            }

            if (mediaIds.Count > 0)
            {
                var medias = await _filesClient.GetFilesByIds(mediaIds);
                foreach (var entity in entities)
                {
                    categoryMedia = entity.Medias?.Where(m => !m.IsPrimary).FirstOrDefault();
                    if (categoryMedia != null)
                    {
                        var media = medias.Where(m => m.Id == categoryMedia.MediaId).FirstOrDefault();
                        if (media != null)
                        {
                            entity.FilePath = media.Formats.Where(f => f.Format == "thumbnail").Select(f => f.FilePath).First();
                            entity.AltText = categoryMedia.AltText;
                        }
                    }
                }
            }
            return entities;
        }
    }
}
