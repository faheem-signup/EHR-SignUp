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
    public class ProviderStateLicenseInfoDto
    {
        public int? StateLicenseId { get; set; }
        public string StateLicenseNo { get; set; }
        public int? StateLicense { get; set; }
        public DateTime? StateLicenseExpiryDate { get; set; }
        public int? StateLicenseProviderId { get; set; }
    }
}
