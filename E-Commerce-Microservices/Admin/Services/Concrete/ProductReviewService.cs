using Admin.Repositories.Abstract;
using Admin.Services.Abstract;
using AutoMapper;
using Common.Dtos.Admin.ProductReview;
using Common.Dtos.Common;
using Common.Entities;
using Common.Utilities;

namespace Admin.Services.Concrete
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IRepository<ProductReview> _reviewRepository;
        private readonly IMapper _mapper;

        public ProductReviewService(IRepository<ProductReview> reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetProductReviewsPaginateDto>> GetAllPaginateAsync(PagedRequest request)
        {
            request.Includes = new List<string>() { "Product" };
            var (reviews, total) = await _reviewRepository.GetAllPaginateAsync(request);

            var response = new PagedResponse<GetProductReviewsPaginateDto>
            {
                Total = total,
                Items = reviews.Select(review => new GetProductReviewsPaginateDto
                {
                    Title = review.Title,
                    Name = review.Name,
                    IsUser = review.UserId!=null?true : false,
                    CreatedAt = DateTimeExtensions.ToPersianDate(review.CreatedAt),
                    ProductName = review.Product.Name,
                    Id = review.Id,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    ProductId = review.ProductId,
                    IsApproved = review.IsApproved  
                }).ToList()
            };

            return response;
        }

        public async Task<ProductReview?> GetByIdAsync(Guid id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductReview>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await _reviewRepository.GetAllByIdsAsync(ids);
        }

        public async Task<ProductReview> AddAsync(CreateProductReviewsRequest request)
        {
            var entity = await _reviewRepository.AddAsync(_mapper.Map<ProductReview>(request));
            await _reviewRepository.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductReview?> UpdateAsync(CreateProductReviewsRequest request)
        {
            ProductReview? entity = null;
            if (request.Id != null)
            {
                entity = await _reviewRepository.GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    _mapper.Map(request, entity);
                    await _reviewRepository.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task DeleteAsync(IEnumerable<ProductReview> brands)
        {
            _reviewRepository.DeleteRange(brands);
            await _reviewRepository.SaveChangesAsync();
        }
    }
}
