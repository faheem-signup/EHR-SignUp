using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientDispensingDosingDto
    {
        public int DispensingDosingId { get; set; }
        public int PatientId { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TherapistId { get; set; }
        public string LastMidVisit { get; set; }
        public string LastDose { get; set; }
        public string TakeHome { get; set; }
        public string MedicatedThru { get; set; }
        public string MedThruDose { get; set; }
        public string LevelOfCare { get; set; }
        public string LastUAResult { get; set; }
        public int Status { get; set; }
        public int MedicationId { get; set; }
        public int ScheduleId { get; set; }
    }
}
