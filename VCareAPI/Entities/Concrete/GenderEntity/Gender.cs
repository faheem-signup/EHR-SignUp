using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.GenderEntity
{
    public class Gender : IEntity
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
