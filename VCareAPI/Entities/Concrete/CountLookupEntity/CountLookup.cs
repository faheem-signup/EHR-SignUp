using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.CountLookupEntity
{
   public class CountLookup : IEntity
    {
        [Key]
        public int CountLookupId { get; set; }
        public string Description { get; set; }
    }
}
