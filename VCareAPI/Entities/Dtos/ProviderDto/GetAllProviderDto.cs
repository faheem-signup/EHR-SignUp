using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class GetAllProviderDto
    {
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public string ProviderEmail { get; set; }
        public string OfficeEmail { get; set; }
        public string PreviousName { get; set; }
        public string Degree { get; set; }
        public string NPINumber { get; set; }
        public string TaxonomyNo { get; set; }
        public string Specialty { get; set; }
        public string PLICarrierName { get; set; }
        public string PLINumber { get; set; }
        public DateTime? PLIExpiryDate { get; set; }
        public int? CAQHID { get; set; }
        public string CAQHUsername { get; set; }
        public string CAQHPassword { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StateLicenseId { get; set; }
        public string StateLicenseNo { get; set; }
        public int? StateLicense { get; set; }
        public DateTime? StateLicenseExpiryDate { get; set; }
        public int? StateLicenseProviderId { get; set; }
        public int? DEAInfoId { get; set; }
        public string DEAInfo { get; set; }
        public DateTime? DEAExpiryDate { get; set; }
        public int? DEAProviderId { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
