using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Common
{
    public class SortOptions
    {
        public string Column { get; set; } = string.Empty;
        public string Order { get; set; } = "asc";
    }
}
