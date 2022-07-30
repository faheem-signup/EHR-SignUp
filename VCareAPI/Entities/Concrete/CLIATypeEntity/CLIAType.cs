using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.CLIATypeEntity
{
    public class CLIAType : IEntity
    {
        [Key]
        public int CLIATypeId { get; set; }
        public string Description { get; set; }
    }
}
