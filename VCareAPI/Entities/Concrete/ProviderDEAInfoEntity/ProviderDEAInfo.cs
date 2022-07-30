using Core.Entities;
using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.Location;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderServiceEntity;
using Entities.Concrete.ReferralProviderTypeEntity;
using Entities.Concrete.RoomEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProviderDEAInfoEntity
{
    public class ProviderDEAInfo : IEntity
    {
        [Key]
        public int DEAInfoId { get; set; }
        public string DEAInfo { get; set; }
        public DateTime? DEAExpiryDate { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

    }
}
 