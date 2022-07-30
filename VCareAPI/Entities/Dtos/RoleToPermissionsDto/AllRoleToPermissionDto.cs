using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.RoleToPermissionsDto
{
   public class AllRoleToPermissionDto
    {

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? PageId { get; set; }
        public int? SubPageId { get; set; }
        public string PageName { get; set; }
    }
}
