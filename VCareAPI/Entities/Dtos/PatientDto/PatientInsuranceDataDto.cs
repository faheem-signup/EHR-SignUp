using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientInsuranceDataDto
    {
        public int AppointmentId { get; set; }
        public string LegalBusinessName { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string AddressLine1 { get; set; }
        public int PrimaryPatientInsuranceId { get; set; }
        public string PrimaryInsuranceName { get; set; }
        public int SecondaryPatientInsuranceId { get; set; }
        public string SecondaryInsuranceName { get; set; }
        public string ProviderName { get; set; }
        public string NPINumber { get; set; }
        public string DEAInfo { get; set; }
        public string StateLicenseNo { get; set; }
    }
}
