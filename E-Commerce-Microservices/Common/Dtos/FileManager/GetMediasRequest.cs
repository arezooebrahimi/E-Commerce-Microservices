using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.FileManager
{
    public class GetMediasRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string? Filter { get; set; }
    }
}
