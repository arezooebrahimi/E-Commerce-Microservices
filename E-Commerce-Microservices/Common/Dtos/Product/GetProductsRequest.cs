

using Common.Dtos.Common;

namespace Common.Dtos.Catalog.Product
{
    public class GetProductsRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public SortOptions? Sort { get; set; }
        public Dictionary<string, FilterOptions>? Filters { get; set; }
    }
}
