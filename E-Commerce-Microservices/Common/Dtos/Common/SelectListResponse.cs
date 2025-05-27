using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Common
{
    public class SelectListResponse
    {
        public Guid Value { get; set; }
        public required string Label { get; set; }
    }
}
