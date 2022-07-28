using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.DiagnosisCodeDto;
using Entities.Dtos.PracticeDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.DiagnoseCode
{
  public class DiagnosisCodeDtoMapping : Profile
    {

        public DiagnosisCodeDtoMapping()
        {
            CreateMap<DiagnosisCode, DiagnosisCodeDto>()
                .ForMember(x => x.DiagnosisId, opt => opt.MapFrom(x => x.DiagnosisId))
                .ForMember(x => x.ICDCategoryId, opt => opt.MapFrom(x => x.ICDCategoryId))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Code))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.ShortDescription, opt => opt.MapFrom(x => x.ShortDescription))
                .ForMember(x => x.ICDGroupName, opt => opt.MapFrom(x => x.ICDCategory.CategoryName));
                
        }
    }
}
