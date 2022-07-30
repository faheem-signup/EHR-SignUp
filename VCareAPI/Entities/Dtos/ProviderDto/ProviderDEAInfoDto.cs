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
    public class ProviderDEAInfoDto
    {
        public int? DEAInfoId { get; set; }
        public string DEAInfo { get; set; }
        public DateTime? DEAExpiryDate { get; set; }
        public int? DEAProviderId { get; set; }
    }
}
