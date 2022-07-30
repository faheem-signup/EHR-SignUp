using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UesrAppDto
{
    public class UserAppPermissionDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalEmail { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsProvider { get; set; }
        public int? PracticeId { get; set; }
        public int? ProviderId { get; set; }
        public List<PermissionDto> permissions { get; set; }
    }
}
