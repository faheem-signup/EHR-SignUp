using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.UserPermissionsDto
{
    public class GetUserToPermissionDto
    {
        public int? RoleToPermissionsId { get; set; }
        public int? UserId { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanView { get; set; }
        public string PageName { get; set; }
        public int? PageId { get; set; }
        public int? RoleId { get; set; }
    }
}
