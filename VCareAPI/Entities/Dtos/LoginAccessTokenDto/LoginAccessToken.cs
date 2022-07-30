using Entities.Concrete.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LoginAccessTokenDto
{
    public class LoginAccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<Permissions> Permissions { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
