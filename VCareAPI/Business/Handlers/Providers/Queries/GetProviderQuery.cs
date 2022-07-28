using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IProviderRepository;
using Entities.Concrete.ProviderBoardCertificationInfoEntity;
using Entities.Concrete.ProviderDEAInfoEntity;
using Entities.Concrete.ProviderEntity;
using Entities.Concrete.ProviderSecurityCheckInfoEntity;
using Entities.Concrete.ProviderStateLicenseInfoEntity;
using Entities.Concrete.ProviderWorkConfigEntity;
using Entities.Concrete.User;
using Entities.Dtos.ProviderDto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Providerhandler.Queries
{
    public class GetProviderQuery : IRequest<IDataResult<GetProviderByIdDto>>
    {
        public int ProviderId { get; set; }

        public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, IDataResult<GetProviderByIdDto>>
        {
            private readonly IProviderRepository _providerRepository;
            private readonly IMapper _mapper;

            public GetProviderQueryHandler(IProviderRepository providerRepository, IMapper mapper)
            {
                _providerRepository = providerRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<GetProviderByIdDto>> Handle(GetProviderQuery request, CancellationToken cancellationToken)
            {
                var provider = await _providerRepository.GetProviderById(request.ProviderId);

                return new SuccessDataResult<GetProviderByIdDto>(provider);
            }
        }
    }
}
