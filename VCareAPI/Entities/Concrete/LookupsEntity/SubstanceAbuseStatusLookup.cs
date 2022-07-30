using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class SubstanceAbuseStatusLookup : IEntity
    {
        [Key]
        public int SubstanceAbuseStatusLookupId { get; set; }
        public string Description { get; set; }
    }
}
