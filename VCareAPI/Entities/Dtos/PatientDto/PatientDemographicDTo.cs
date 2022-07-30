using Entities.Concrete.PatientEducationEntity;
using Entities.Dtos.AppointmentDto;
using Entities.Dtos.FormTemplateDto;
using Entities.Dtos.PatientCommunicationDto;
using Entities.Dtos.PatientDocumentsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.PatientDto
{
    public class PatientDemographicDTo
    {
        public PatientDto PatientDetail { get; set; }
        public PatientAdditionalInfoDto PatientAdditionalInfoDetail { get; set; }
        public AppointmentDTO AppointmentDetail { get; set; }
        public Entities.Dtos.PatientVitalsDto.PatientVitalsDto PatientVitalsDetail { get; set; }
        public Entities.Dtos.PatientDiagnosisCodeDto.PatientDiagnosisCodeDto PatientDiagnosisCodeDetail { get; set; }
        public List<Entities.Dtos.PatientInsurancesDto.PatientInsurancesDto> PatientInsuranceList { get; set; }
        public IEnumerable<GetPatientCommunicationListDto> PatientCommunicationList { get; set; }
        public IEnumerable<PatientDocumentDto> PatientDocumentList { get; set; }
        public IEnumerable<PatientEducationDocument> PatientEducationDocumentList { get; set; }
        public IEnumerable<NotesDto> PatientNotesList { get; set; }

    }
}
