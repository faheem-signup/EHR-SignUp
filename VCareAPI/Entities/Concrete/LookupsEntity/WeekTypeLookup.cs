using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.LookupsEntity
{
    public class WeekTypeLookup : IEntity
    {
        [Key]
        public int WeekTypeId { get; set; }
        public string WeekTypeName { get; set; }
    }
}
