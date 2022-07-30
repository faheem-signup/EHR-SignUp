using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientAdditionalInfoDto
    {
        public int? PatientId { get; set; }
        //MORE INFO
        public int? SmokingStatus { get; set; }
        public int? Packs { get; set; }
        public int? HospitalizationStatus { get; set; }
        public DateTime? LastHospitalizationDate { get; set; }
        public DateTime? DisabilityDate { get; set; }
        public int? DisabilityStatus { get; set; }
        public DateTime? DeathDate { get; set; }
        public string DeathReason { get; set; }
        public int? SubstanceAbuseStatus { get; set; }
        public int? Alcohol { get; set; }
        public int? IllicitSubstances { get; set; }
        //ACCIDENT DETAILS
        public int? EmploymentStatus { get; set; }
        public int? WorkStatus { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerPhone { get; set; }
        public DateTime? AccidentDate { get; set; }
        public int? AccidentType { get; set; }
        public string Wc { get; set; }
        //PROVIDER DETAILS
        public string AttendingPhysician { get; set; }
        public string SupervisingProvider { get; set; }
        public string LocationName { get; set; }
        public int? LocationId { get; set; }
        public string IllicitSubstancesName { get; set; }
        public string SubstanceAbuseStatusName { get; set; }
        public string AlcoholName { get; set; }
        public List<PatientProvideReferringDto> _patientProvideReferring { get; set; }
        //public int? PCPName { get; set; }
        //public string Pharmacy { get; set; }
        //public int? ReferringAgency { get; set; }
        //public int? DrugsAgency { get; set; }
        //public int? ProbationOfficer { get; set; }
        //public int? SubstanceAbuseStatus { get; set; }
        //public int? Alcohol { get; set; }
        //public int? IllicitSubstances { get; set; }

        public string EmploymentStatusDescription { get; set; }
        public string WorkStatusDescription { get; set; }
        public string AccidentTypeDescription { get; set; }
        public string SmokingStatusDescription { get; set; }
        public string PacksDescription { get; set; }
        public string HospitalizationStatusDescription { get; set; }
        public string PatientDisabilityStatusDescription { get; set; }
        public string SubstanceAbuseStatusDescription { get; set; }
        public string AlcoholDescription { get; set; }
        public string IllicitSubstancesDescription { get; set; }
    }
}
