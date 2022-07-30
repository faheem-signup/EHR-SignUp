using Entities.Concrete.CityStateLookupEntity;
using Entities.Concrete.Location;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderServiceEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.RoomEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class ProviderBoardCertificationInfoDto
    {
        public int? BoardCertificationId { get; set; }
        public string BoardCertified { get; set; }
        public string CertBody { get; set; }
        public string CertNumber { get; set; }
        public DateTime? CertExpiryDate { get; set; }
        public int? ProviderId { get; set; }
    }
}
