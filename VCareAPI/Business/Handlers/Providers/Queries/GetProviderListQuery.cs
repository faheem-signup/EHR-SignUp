using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProviderRepository;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Dtos.ProviderDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providers.Queries
{
    public class GetProviderListQuery : BasePaginationQuery<IDataResult<IEnumerable<ProviderListDto>>>
    {
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int? StatusId { get; set; }
        public int? LocationId { get; set; }

        public class GetProviderListQueryHandler : IRequestHandler<GetProviderListQuery, IDataResult<IEnumerable<ProviderListDto>>>
        {
            private readonly IProviderRepository _providerRepository;
            private readonly IMapper _mapper;
            public GetProviderListQueryHandler(IProviderRepository providerRepository, IMapper mapper)
            {
                _providerRepository = providerRepository;
                _mapper = mapper;
                var Config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Provider, ProvidersDto>();
                    //.ForMember(p => p.ProviderBoardCertificationInfo, conf => conf.MapFrom(value => value.ProviderBoardCertificationInfos))
                    //.ForMember(p => p.ProviderSecurityCheckInfo, conf => conf.MapFrom(value => value.ProviderSecurityCheckInfos))
                    //.ForMember(p => p.ProviderStateLicenseInfo, conf => conf.MapFrom(value => value.ProviderStateLicenseInfos))
                    //.ForMember(p => p.ProviderDEAInfo, conf => conf.MapFrom(value => value.ProviderDEAInfos))
                    //.ForMember(p => p.ProviderWorkConfig, conf => conf.MapFrom(value => value.ProviderWorkConfigs))
                    //.ForMember(p => p.LocationName, conf => conf.MapFrom(value => value.Locations.LocationName));
                    
                    cfg.CreateMap<ProviderWorkConfig,ProviderWorkConfigDto>();
                    cfg.CreateMap<ProviderStateLicenseInfo,ProviderStateLicenseInfoDto>();
                    cfg.CreateMap<ProviderBoardCertificationInfo, ProviderBoardCertificationInfoDto>();
                    cfg.CreateMap<ProviderSecurityCheckInfo, ProviderSecurityCheckInfoDto>();
                    cfg.CreateMap<ProviderDEAInfo, ProviderDEAInfoDto>();
                });
                _mapper = Config.CreateMapper();

            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProviderListDto>>> Handle(GetProviderListQuery request, CancellationToken cancellationToken)
            {
                var list = await _providerRepository.GetProviderBySearchParams(request.LastName, request.Type, request.Title, request.StatusId, request.LocationId);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                //var mappedData = list.Select(x => _mapper.Map<ProvidersDto>(x)).ToList();
                return new PagedDataResult<IEnumerable<ProviderListDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
