using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.TaxTypeEntity
{
   public class TaxType : IEntity
    {
        [Key]
        public int TaxTypeId { get; set; }
        public string Description { get; set; }
    }
}
