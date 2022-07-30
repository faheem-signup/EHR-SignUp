using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AuthDto
{
    public class AuthDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public object ResponseData { get; set; }
    }
}
