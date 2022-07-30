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

namespace Entities.Concrete.ProviderStateLicenseInfoEntity
{
    public class ProviderStateLicenseInfo : IEntity
    {
        [Key]
        public int StateLicenseId { get; set; }
        public string StateLicenseNo { get; set; }
        public int? StateLicense { get; set; }
        public DateTime? StateLicenseExpiryDate { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        [ForeignKey("StateLicense")]
        public virtual CityStateLookup States { get; set; }

    }
}
 