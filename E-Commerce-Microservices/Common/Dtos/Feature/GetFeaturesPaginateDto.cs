using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Catalog.Feature
{
    public class GetFeaturesPaginateDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public List<string>? OptionsName { get; set; }
    }
}
