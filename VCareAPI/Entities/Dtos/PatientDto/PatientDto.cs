using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime? DOB { get; set; }
        public int? Gender { get; set; }
        public string SSN { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? County { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? ZIP { get; set; }
        public int? Race { get; set; }
        public int? Ethnicity { get; set; }
        public int? MaritalStatus { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public int? PreferredCom { get; set; }
        public int? PreferredLanguage { get; set; }
        public string EmergencyConName { get; set; }
        public string EmergencyConAddress { get; set; }
        public string EmergencyConRelation { get; set; }
        public string EmergencyConPhone { get; set; }
        public string Comment { get; set; }
        public byte[] PatientImage { get; set; }
        public string PatientImagePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? StatusId { get; set; }
        public string GenderName { get; set; }
        public string MaritalStatusDescription { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public List<PatientGuardianDto> PatientGuardianList { get; set; }
    }
}
