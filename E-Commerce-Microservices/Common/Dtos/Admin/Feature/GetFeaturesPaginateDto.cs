using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Admin.Feature
{
    public class GetFeaturesPaginateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required bool IsFilter { get; set; }
        public List<string>? OptionsName { get; set; }
    }
}
