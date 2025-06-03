using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Admin.FeatureOption
{
    public class GetFeatureOptionsPaginateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Slug { get; set; }
        public string? FeatureName { get; set; }
    }
}
