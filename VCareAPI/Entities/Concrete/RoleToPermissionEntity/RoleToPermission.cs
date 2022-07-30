using Core.Entities;
using Entities.Concrete.Permission;
using Entities.Concrete.Role;
using Entities.Concrete.TBLPageEntity;
using Entities.Concrete.TblSubPageEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.RoleToPermissionEntity
{
    public class RoleToPermission : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? PageId { get; set; }
        public int? SubPageId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles roles { get; set; }

        [ForeignKey("PageId")]
        public virtual TblPage tblPage { get; set; }

        [ForeignKey("SubPageId")]
        public virtual TblSubPage _tblSubPage { get; set; }
    }
}
