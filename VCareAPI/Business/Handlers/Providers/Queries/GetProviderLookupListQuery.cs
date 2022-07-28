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
using Entities.Dtos.LookUpDto;
using Entities.Dtos.ProviderDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providers.Queries
{
    public class GetProviderLookupListQuery : IRequest<IDataResult<IEnumerable<LookupDto>>>
    {
        public class GetProviderLookupListQueryHandler : IRequestHandler<GetProviderLookupListQuery, IDataResult<IEnumerable<LookupDto>>>
        {
            private readonly IProviderRepository _providerRepository;

            public GetProviderLookupListQueryHandler(IProviderRepository providerRepository)
            {
                _providerRepository = providerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LookupDto>>> Handle(GetProviderLookupListQuery request, CancellationToken cancellationToken)
            {
                var providerLookupList = await _providerRepository.GetProviderLookupList();
                return new SuccessDataResult<IEnumerable<LookupDto>>(providerLookupList.ToList());
            }
        }
    }
}
