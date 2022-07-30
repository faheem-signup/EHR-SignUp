using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.ReferralProviderTypeEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProviderServiceEntity
{
    public class ProviderService : IEntity
    {
        [Key]
        public int ProviderServiceId { get; set; }
        public string ServiceDescription { get; set; }
    }
}
 