using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.UserTypeEntity
{
    public class UserType : IEntity
    {
        [Key]
        public int UserTypeId { get; set; }
        public string Description { get; set; }
    }
}
