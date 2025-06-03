using Common.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Admin.Product
{
    public class GetProductsPaginateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? BrandName { get; set; }
        public long Price { get; set; }
        public Status Status { get; set; }
        public short Type { get; set; }
        public StockStatus StockStatus { get; set; }
    }
}
