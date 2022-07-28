using AutoMapper;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.Location;
using Entities.Dtos.AppointmentDto;
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


            CreateMap<Appointment, AppointmentScheduleDto>()
               .ForMember(x => x.AppointmentId, opt => opt.MapFrom(x => x.AppointmentId))
               .ForMember(x => x.AllowGroupPatient, opt => opt.MapFrom(x => x.AllowGroupPatient))

               .ForMember(x => x.AppointmentStatus, opt => opt.MapFrom(x => x.AppointmentStatus))
               .ForMember(x => x.TimeFrom, opt => opt.MapFrom(x => x.TimeFrom))
               .ForMember(x => x.TimeTo, opt => opt.MapFrom(x => x.TimeTo))
               .ForMember(x => x.AppointmentDate, opt => opt.MapFrom(x => x.AppointmentDate))
               .ForMember(x => x.Notes, opt => opt.MapFrom(x => x.Notes))
               .ForMember(x => x.VisitReason, opt => opt.MapFrom(x => x.VisitReason))
               .ForMember(x => x.RoomNo, opt => opt.MapFrom(x => x.RoomNo))
               .ForMember(x => x.VisitType, opt => opt.MapFrom(x => x.VisitType))
               .ForMember(x => x.PatientId, opt => opt.MapFrom(x => x.PatientId))
               .ForMember(x => x.GroupAppointmentReason, opt => opt.MapFrom(x => x.GroupAppointmentReason))
               .ForMember(x => x.IsRecurringAppointment, opt => opt.MapFrom(x => x.IsRecurringAppointment))
               .ForMember(x => x.IsFollowUpAppointment, opt => opt.MapFrom(x => x.IsFollowUpAppointment));


            //.ForMember(x => x.ProviderName, opt => opt.MapFrom(x => x.Provider.FirstName + " " + x.Provider.LastName))
            //   .ForMember(x => x.LocationName, opt => opt.MapFrom(x => x.Locations.LocationName))
            //   .ForMember(x => x.RoomName, opt => opt.MapFrom(x => x.Room.RoomName))
            //   .ForMember(x => x.AppointmentReason, opt => opt.MapFrom(x => x.AppointmentReasons.AppointmentReasonDescription))
            //   .ForMember(x => x.AppointmentTypeName, opt => opt.MapFrom(x => x._appointmentTypes.AppointmentTypeName))
            //   .ForMember(x => x.ServiceProfileName, opt => opt.MapFrom(x => x._serviceProfile.ServiceProfileName));

        }
    }
}
