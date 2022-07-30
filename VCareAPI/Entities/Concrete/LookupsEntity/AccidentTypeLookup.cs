using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class AccidentTypeLookup : IEntity
    {
        [Key]
        public int AccidentTypeLookupId { get; set; }
        public string Description { get; set; }
    }
}
