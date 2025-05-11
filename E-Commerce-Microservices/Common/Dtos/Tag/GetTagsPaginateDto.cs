using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Tag
{
    public class GetTagsPaginateDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
    }
}
