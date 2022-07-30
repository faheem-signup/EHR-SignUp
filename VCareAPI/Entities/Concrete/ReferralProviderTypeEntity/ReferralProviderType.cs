using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.ReferralProviderTypeEntity
{
    public class ReferralProviderType : IEntity
    {
        [Key]
        public int ReferralProviderTypeId { get; set; }
        public string TypeDescription { get; set; }
    }
}
