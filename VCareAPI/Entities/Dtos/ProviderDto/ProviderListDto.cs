using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class ProviderListDto
    {
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string ProviderEmail { get; set; }
        public string OfficeEmail { get; set; }
        public string NPINumber { get; set; }
        public int? CAQHID { get; set; }
        public int? LocationId { get; set; }
        public int? AssignedLocationId { get; set; }
        public string LocationName { get; set; }
        public string Degree { get; set; }
        public List<ProviderDEAInfoDto> providerDEAInfoList { get; set; }
        public List<ProviderStateLicenseInfoDto> providerStateLicenseInfoList { get; set; }

    }
}
