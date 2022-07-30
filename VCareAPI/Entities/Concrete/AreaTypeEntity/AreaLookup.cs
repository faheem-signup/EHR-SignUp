using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.AreaTypeEntity
{
   public class AreaLookup: IEntity
    {
        [Key]
        public int AreaId { get; set; }
        public string AreaName { get; set; }

    }
}
