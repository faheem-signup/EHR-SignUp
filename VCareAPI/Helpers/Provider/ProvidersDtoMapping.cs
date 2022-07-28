using AutoMapper;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.Location;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.LocationDto;
using Entities.Dtos.PatientDto;
using Entities.Dtos.ProviderDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Providers
{
    public class ProvidersDtoMapping : Profile
    {
        public ProvidersDtoMapping()
        {
            CreateMap<Provider, ProvidersDto>();
                //.ForMember(p => p.ProviderBoardCertificationInfo, conf => conf.MapFrom(value => value.ProviderBoardCertificationInfos))
                //.ForMember(p => p.ProviderSecurityCheckInfo, conf => conf.MapFrom(value => value.ProviderSecurityCheckInfos))
                //.ForMember(p => p.ProviderStateLicenseInfo, conf => conf.MapFrom(value => value.ProviderStateLicenseInfos))
                //.ForMember(p => p.ProviderDEAInfo, conf => conf.MapFrom(value => value.ProviderDEAInfos))
                //.ForMember(p => p.ProviderWorkConfig, conf => conf.MapFrom(value => value.ProviderWorkConfigs))
                //.ForMember(p => p.LocationName, conf => conf.MapFrom(value => value.Locations.LocationName));
            CreateMap<ProviderWorkConfig, ProviderWorkConfigDto>();
            CreateMap<ProviderStateLicenseInfo, ProviderStateLicenseInfoDto>();
            CreateMap<ProviderBoardCertificationInfo, ProviderBoardCertificationInfoDto>();
            CreateMap<ProviderSecurityCheckInfo, ProviderSecurityCheckInfoDto>();
            CreateMap<ProviderDEAInfo, ProviderDEAInfoDto>();
        }
    }
}
