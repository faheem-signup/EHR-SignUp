using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IServiceProfileRepository;
using Entities.Concrete.Role;
using Entities.Concrete.ServiceProfileEntity;
using Entities.Dtos.ServiceProfileDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ServiceProfiles.Queries
{
    public class GetServiceProfileLookupListQuery : IRequest<IDataResult<IEnumerable<ServiceProfilesDto>>>
    {
        public class GetServiceProfileLookupListQueryHandler : IRequestHandler<GetServiceProfileLookupListQuery, IDataResult<IEnumerable<ServiceProfilesDto>>>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;
            public GetServiceProfileLookupListQueryHandler(IServiceProfileRepository serviceProfileRepository)
            {
                _serviceProfileRepository = serviceProfileRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ServiceProfilesDto>>> Handle(GetServiceProfileLookupListQuery request, CancellationToken cancellationToken)
            {
                var list = await _serviceProfileRepository.GetAllServiceProfile();
                 return new SuccessDataResult<IEnumerable<ServiceProfilesDto>>(list.ToList());
            }
        }
    }
}
