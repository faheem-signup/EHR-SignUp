using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.Permission
{
   public class Permissions : IEntity
    {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }
    }
}
