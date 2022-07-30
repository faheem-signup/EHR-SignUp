using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UserPermissionsDto
{
   public class UserPermissionsDto
    {
        public int? RoleToPermissionId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanDelete { get; set; }
    }
}
