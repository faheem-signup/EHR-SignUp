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
    public class ProviderSecurityCheckInfoDto
    {
        public int? SecurityCheckId { get; set; }
        public string SecurityCheck { get; set; }
        public int? FBIState { get; set; }
        public string StatePoliceClearance { get; set; }
        public int? ProviderClinicalInfoId { get; set; }
    }
}
