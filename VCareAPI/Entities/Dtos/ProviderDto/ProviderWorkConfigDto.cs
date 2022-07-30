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
    public class ProviderWorkConfigDto
    {
        public int? Id { get; set; }
        public int? ProviderId { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime? BreakShiftWorkFrom { get; set; }
        public DateTime? BreakShiftWorkTo { get; set; }
        public TimeSpan SlotSize { get; set; }
        public bool? IsBreak { get; set; }
    }
}
