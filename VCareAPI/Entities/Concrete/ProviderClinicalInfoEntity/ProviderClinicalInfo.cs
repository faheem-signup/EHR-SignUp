using Core.Entities;
using Entities.Concrete.Location;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderServiceEntity;
using Entities.Concrete.RoomEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete.ProviderClinicalInfoEntity
{
    public class ProviderClinicalInfo : IEntity
    {
        [Key]
        public int ProviderClinicalInfoId { get; set; }
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
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider provider { get; set; }

        [ForeignKey("ProviderServiceId")]
        public virtual ProviderService providerService { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room room { get; set; }
        [ForeignKey("LocationId")]
        public virtual Locations location { get; set; }
    }
}
