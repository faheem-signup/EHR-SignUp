using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class PacksLookup : IEntity
    {
        [Key]
        public int PacksLookupId { get; set; }
        public string Description { get; set; }
    }
}
