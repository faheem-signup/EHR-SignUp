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

namespace Entities.Concrete.ProviderBoardCertificationInfoEntity
{
    public class ProviderBoardCertificationInfo : IEntity
    {
        [Key]
        public int BoardCertificationId { get; set; }
        public string BoardCertified { get; set; }
        public string CertBody { get; set; }
        public string CertNumber { get; set; }
        public DateTime? CertExpiryDate { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }

    }
}
 