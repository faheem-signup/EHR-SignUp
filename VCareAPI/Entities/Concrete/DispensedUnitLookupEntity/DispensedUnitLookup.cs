using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.DispensedUnitLookupEntity
{
    public class DispensedUnitLookup : IEntity
    {
        [Key]
        public int DispensedUnitLookupId { get; set; }
        public string Description { get; set; }
    }
}
