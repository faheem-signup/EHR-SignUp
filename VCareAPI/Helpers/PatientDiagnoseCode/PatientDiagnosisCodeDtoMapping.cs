using AutoMapper;
using Entities.Concrete.PatientDiagnosisCodeEntity;
using Entities.Concrete.PatientVitalsEntity;
using Entities.Dtos.PatientDiagnosisCodeDto;
using Entities.Dtos.PatientVitalsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.PatientDiagnoseCode
{
    public class PatientDiagnosisCodeDtoMapping : Profile
    {
        public PatientDiagnosisCodeDtoMapping()
        {
            CreateMap<PatientDiagnosisCode, PatientDiagnosisCodeDto>()
                .ForMember(x => x.ProviderName, opt => opt.MapFrom(x => x.Provider.FirstName + " " + x.Provider.LastName))
                .ForMember(x => x.PatientDiagnosisCodeId, opt => opt.MapFrom(x => x.PatientDiagnosisCodeId))
                .ForMember(x => x.DiagnosisDate, opt => opt.MapFrom(x => x.DiagnosisDate))
                .ForMember(x => x.ResolvedDate, opt => opt.MapFrom(x => x.ResolvedDate))
                .ForMember(x => x.DiagnoseCodeType, opt => opt.MapFrom(x => x.DiagnoseCodeType))
                .ForMember(x => x.SNOMEDCode, opt => opt.MapFrom(x => x.SNOMEDCode))
                .ForMember(x => x.SNOMEDCodeDesctiption, opt => opt.MapFrom(x => x.SNOMEDCodeDesctiption))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments));
        }
    }
}
