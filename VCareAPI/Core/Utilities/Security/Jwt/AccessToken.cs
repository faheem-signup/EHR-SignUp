using System;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        //public List<string> Permissions { get; set; }
        //public int RoleId { get; set; }
        //public string RoleName { get; set; }
    }
}
