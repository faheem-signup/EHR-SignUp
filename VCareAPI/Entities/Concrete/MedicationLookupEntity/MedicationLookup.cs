using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.MedicationLookupEntity
{
    public class MedicationLookup : IEntity
    {
        [Key]
        public int MedicationLookupId { get; set; }
        public string Description { get; set; }
    }
}
