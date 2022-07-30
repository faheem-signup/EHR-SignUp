using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class PracticeAssignmentLookup :IEntity
    {
        [Key]
        public int PracticeAssignmentLookupId { get; set; }
        public string Description { get; set; }
    }
}
