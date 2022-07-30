using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class GetProviderByIdDto
    {
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
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
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string CellNumber { get; set; }
        public int? ProviderClinicalInfoId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? ReHiringDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? ProviderServiceId { get; set; }
        public string ChildAbuseCertificate { get; set; }
        public DateTime? ChildAbuseCertExpiryDate { get; set; }
        public string MandatedReporterCertificate { get; set; }
        public DateTime? MandatedReportExpiryDate { get; set; }
        public int? RoomId { get; set; }
        public string CredentialedInsurances { get; set; }
        public decimal? FlatRate { get; set; }
        public decimal? ProcedureBasedRate { get; set; }
        public decimal? HourlyRate { get; set; }
        public string ContinuingEducation { get; set; }
        public decimal? CompletedHours { get; set; }
        public int? ClinicalInfoProviderId { get; set; }
        public List<GetProviderWorkConfigDto> ProviderWorkConfig { get; set; }
        public List<ProviderBoardCertificationInfoDto> ProviderBoardCertificationInfo { get; set; }
        public List<ProviderDEAInfoDto> ProviderDEAInfo { get; set; }
        public List<ProviderSecurityCheckInfoDto> ProviderSecurityCheckInfo { get; set; }
        public List<ProviderStateLicenseInfoDto> ProviderStateLicenseInfo { get; set; }
    }
}
