using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.POSEntity
{
    public class POS : IEntity
    {
        [Key]
        public int POSId { get; set; }
        public string Description { get; set; }
    }
}
