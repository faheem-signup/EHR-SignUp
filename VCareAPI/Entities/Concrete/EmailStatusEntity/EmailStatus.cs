using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.EmailStatusEntity
{
    public class EmailStatus : IEntity
    {
        [Key]
        public int EmailStatusId { get; set; }
        public string EmailStatusName { get; set; }
    }
}
