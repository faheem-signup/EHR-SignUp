using AutoMapper;
using Entities.Concrete.PatientVitalsEntity;
using Entities.Dtos.PatientVitalsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.PatientVital
{
    public class PatientVitalsDtoMapping : Profile
    {
        public PatientVitalsDtoMapping()
        {
            CreateMap<PatientVitals, PatientVitalsDto>()
                .ForMember(x => x.ProvderName, opt => opt.MapFrom(x => x.Provider.FirstName + " " + x.Provider.LastName))
                .ForMember(x => x.PatientVitalsId, opt => opt.MapFrom(x => x.PatientVitalsId))
                .ForMember(x => x.VisitDate, opt => opt.MapFrom(x => x.VisitDate))
                .ForMember(x => x.Height, opt => opt.MapFrom(x => x.Height))
                .ForMember(x => x.Weight, opt => opt.MapFrom(x => x.Weight))
                .ForMember(x => x.BMI, opt => opt.MapFrom(x => x.BMI))
                .ForMember(x => x.SystolicBP, opt => opt.MapFrom(x => x.SystolicBP))
                .ForMember(x => x.DiaSystolicBP, opt => opt.MapFrom(x => x.DiaSystolicBP))
                .ForMember(x => x.HeartRate, opt => opt.MapFrom(x => x.HeartRate))
                .ForMember(x => x.RespiratoryRate, opt => opt.MapFrom(x => x.RespiratoryRate))
                .ForMember(x => x.Temprature, opt => opt.MapFrom(x => x.Temprature))
                .ForMember(x => x.PainScale, opt => opt.MapFrom(x => x.PainScale))
                .ForMember(x => x.HeadCircumference, opt => opt.MapFrom(x => x.HeadCircumference))
                .ForMember(x => x.Comment, opt => opt.MapFrom(x => x.Comment));
        }
    }
}
