using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.PracticeTypeEntity
{
    public class PracticeType : IEntity
    {
        [Key]
        public int PracticeTypeId { get; set; }
        public string Description { get; set; }
    }
}
