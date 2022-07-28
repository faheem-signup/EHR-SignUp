using AutoMapper;
using Entities.Concrete;
using Entities.Concrete.ContactEntity;
using Entities.Dtos.ContactDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Contacts
{
    public class ContactDtoMapping : Profile
    {
        public ContactDtoMapping()
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(x => x.ContactId, opt => opt.MapFrom(x => x.ContactId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.Fax, opt => opt.MapFrom(x => x.Fax))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address))
                .ForMember(x => x.CityName, opt => opt.MapFrom(x => x.Cities.CityName))
                .ForMember(x => x.StateName, opt => opt.MapFrom(x => x.states.State))
                .ForMember(x => x.StateCode, opt => opt.MapFrom(x => x.states.StateCode))
                .ForMember(x => x.ZipCode, opt => opt.MapFrom(x => x.zipcode.ZipCode))
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.status.StatusName))
                .ForMember(x => x.ContactTypeDescription, opt => opt.MapFrom(x => x.ContactType.Description));
        }
    }
}
