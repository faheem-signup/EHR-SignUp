using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ProgramLookupEntity
{
    public class ProgramLookup : IEntity
    {
        [Key]
        public int ProgramLookupId { get; set; }
        public string Description { get; set; }
    }
}
