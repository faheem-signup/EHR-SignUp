using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class PreferredCommsLookup : IEntity
    {
        [Key]
        public int PreferredCommsLookupId { get; set; }
        public string Description { get; set; }
    }
}
