using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.TherapistLookupEntity
{
    public class TherapistLookup : IEntity
    {
        [Key]
        public int TherapistLookupId { get; set; }
        public string Description { get; set; }
    }
}
