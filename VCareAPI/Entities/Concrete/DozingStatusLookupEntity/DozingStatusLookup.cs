using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.DozingStatusLookupEntity
{
    public class DozingStatusLookup : IEntity
    {
        [Key]
        public int DozingStatusLookupId { get; set; }
        public string Description { get; set; }
    }
}
