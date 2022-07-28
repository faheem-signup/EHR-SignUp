using AutoMapper;
using Entities.Concrete.Location;
using Entities.Dtos.LocationDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Location
{
    public class LocationDtoMapping : Profile
    {

        public LocationDtoMapping()
        {
            CreateMap<Locations, LocationDto>()
                .ForMember(x => x.PracticeName, opt => opt.MapFrom(x => x.Practice.FirstName + " " + x.Practice.LastName))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.states.State))
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.Cities.CityName))
                .ForMember(x => x.ZIP, opt => opt.MapFrom(x => x.zipcode.ZipCode))
                .ForMember(x => x.LocationId, opt => opt.MapFrom(x => x.LocationId))
                .ForMember(x => x.LocationName, opt => opt.MapFrom(x => x.LocationName))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));
        }
    }
}
