using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IServiceProfileRepository;
using Entities.Concrete.Role;
using Entities.Concrete.ServiceProfileEntity;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ServiceProfiles.Queries
{
   
    public class GetServiceProfileQuery : IRequest<IDataResult<IEnumerable<ServiceProfile>>>
    {
        public string Row_Id { get; set; }
        public class GetServiceProfileQueryHandler : IRequestHandler<GetServiceProfileQuery, IDataResult<IEnumerable<ServiceProfile>>>
        {
            private readonly IServiceProfileRepository _serviceProfileRepository;
            public GetServiceProfileQueryHandler(IServiceProfileRepository serviceProfileRepository)
            {
                _serviceProfileRepository = serviceProfileRepository;
            }

            //[SecuredOperation(Priority = 1)]
            // [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<ServiceProfile>>> Handle(GetServiceProfileQuery request, CancellationToken cancellationToken)
            {
                var serviceProfile = await _serviceProfileRepository.GetByRowId(request.Row_Id);
                return new SuccessDataResult<IEnumerable<ServiceProfile>>(serviceProfile);
            }
        }
    }

}
