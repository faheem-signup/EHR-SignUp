using AutoMapper;
using Entities.Concrete.AssignPracticeInsuranceEntity;
using Entities.Concrete.Location;
using Entities.Dtos.AssignPracticeInsurancesDto;
using Entities.Dtos.LocationDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Location
{
    public class AssignPracticeInsuranceDtoMapping : Profile
    {

        public AssignPracticeInsuranceDtoMapping()
        {
            CreateMap<AssignPracticeInsurance, AssignPracticeInsuranceDto>()
                .ForMember(x => x.InsuranceName, opt => opt.MapFrom(x => x.insurance.Name))
                .ForMember(x => x.InsuranceType, opt => opt.MapFrom(x => x.insurance.insurancePayerType.PayerTypeDescription))
                .ForMember(x => x.PracticeInsuranceId, opt => opt.MapFrom(x => x.PracticeInsuranceId))
                .ForMember(x => x.PracticeId, opt => opt.MapFrom(x => x.PracticeId))
                .ForMember(x => x.InsuranceId, opt => opt.MapFrom(x => x.InsuranceId));
        }
    }
}
