using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class ProductTag
    {
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
