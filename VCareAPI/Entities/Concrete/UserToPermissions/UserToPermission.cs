using Core.Entities;
using Entities.Concrete.Permission;
using Entities.Concrete.Role;
using Entities.Concrete.RoleToPermissionEntity;
using Entities.Concrete.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.UserToPermissions
{
   public class UserToPermission :IEntity
    {
        [Key]
        public int UserToPermissionId { get; set; }
        public int? UserId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanSearch { get; set; }
        public bool? CanDelete { get; set; }
        public int? RoleToPermissionsId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserApp userApp { get; set; }
        [ForeignKey("RoleToPermissionsId")]
        public virtual RoleToPermission RoleToPermission { get; set; }
    }
}
