using AutoMapper;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using Entities.Dtos.PatientDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Location
{
    public class AppointmentsDtoMapping : Profile
    {
        public AppointmentsDtoMapping()
        {
            CreateMap<Appointment, PatientAppointmentDto>()
                .ForMember(x => x.PatientName, opt => opt.MapFrom(x => x.Patients.FirstName + " " + x.Patients.LastName))
                .ForMember(x => x.ProviderName, opt => opt.MapFrom(x => x.Provider.FirstName + " " + x.Provider.LastName))
                .ForMember(x => x.PhoneNo, opt => opt.MapFrom(x => x.Patients.CellPhone))
                .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Locations.LocationName))
                .ForMember(x => x.AppointmentStatus, opt => opt.MapFrom(x => x.AppointmentStatus))
                .ForMember(x => x.TimeFrom, opt => opt.MapFrom(x => x.TimeFrom))
                .ForMember(x => x.TimeTo, opt => opt.MapFrom(x => x.TimeTo))
                .ForMember(x => x.AppointmentDate, opt => opt.MapFrom(x => x.AppointmentDate))
                .ForMember(x => x.Notes, opt => opt.MapFrom(x => x.Notes))
                .ForMember(x => x.VisitReason, opt => opt.MapFrom(x => x.VisitReason));
        }
    }
}
