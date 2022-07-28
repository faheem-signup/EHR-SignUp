using AutoMapper;
using Entities.Concrete.Location;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Dtos.LocationDto;
using Entities.Dtos.PatientCommunicationDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Location
{
    public class PatientCommunicationDtoMapping : Profile
    {

        public PatientCommunicationDtoMapping()
        {
            CreateMap<PatientCommunication, PatientCommunicationDto>()
                .ForMember(x => x.CommunicateBy, opt => opt.MapFrom(x => x._userApp.FirstName + " " + x._userApp.LastName))
                .ForMember(x => x.CommunicationDate, opt => opt.MapFrom(x => x.CommunicationDate))
                .ForMember(x => x.CommunicationTime, opt => opt.MapFrom(x => x.CommunicationTime))
                .ForMember(x => x.CallDetailTypeId, opt => opt.MapFrom(x => x._communicationCallDetailType.CallDetail))
                .ForMember(x => x.CallDetailDescription, opt => opt.MapFrom(x => x.CallDetailDescription));
        }
    }
}
