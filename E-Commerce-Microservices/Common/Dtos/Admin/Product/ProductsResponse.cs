using Common.Entities;

namespace Common.Dtos.Admin.Product
{
    public class ProductsResponse
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public long? Price { get; set; }
        public long? SalePrice { get; set; }
        public double Raiting  { get; set; }
        public long ReviewsCount { get; set; }
    }


    public class ProductsDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public short Type { get; set; }
        public long? Price { get; set; }
        public long? SalePrice { get; set; }
        public DateTime? DateOnSaleFrom { get; set; }
        public DateTime? DateOnSaleTo { get; set; }
        public required ICollection<ProductVariable> Variables { get; set; }
    }
}
