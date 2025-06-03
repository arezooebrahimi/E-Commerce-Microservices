using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Auth
{
    public class LogoutRequest
    {
        public required string RefreshToken { get; set; }
    }
}
