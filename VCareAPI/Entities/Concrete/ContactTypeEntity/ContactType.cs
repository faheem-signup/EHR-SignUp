using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ContactTypeEntity
{
   public class ContactType :IEntity
    {
        [Key]
        public int ContactTypeId { get; set; }
        public string Description { get; set; }
    }
}
