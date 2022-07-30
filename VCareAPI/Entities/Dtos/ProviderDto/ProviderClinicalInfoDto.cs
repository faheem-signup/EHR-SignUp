using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProviderDto
{
    public class ProviderClinicalInfoDto
    {
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
        public int? ProviderId { get; set; }
        public string CredentialedInsurances { get; set; }
        public decimal? FlatRate { get; set; }
        public decimal? ProcedureBasedRate { get; set; }
        public decimal? HourlyRate { get; set; }
        public string ContinuingEducation { get; set; }
        public decimal? CompletedHours { get; set; }
        public List<ProviderSecurityCheckInfo> providerSecurityCheckInfo { get; set; }
    }
}
