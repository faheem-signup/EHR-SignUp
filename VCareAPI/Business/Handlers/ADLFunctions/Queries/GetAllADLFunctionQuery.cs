using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ADLFunctionRepository;
using DataAccess.Abstract.IADLLookupRepository;
using Entities.Concrete.ADLEntity;
using Entities.Dtos.ADLFunctionDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ADLFunctions.Queries
{
    public class GetAllADLFunctionQuery : IRequest<IDataResult<IEnumerable<GetADLListDto>>>
    {
        public int ProviderId { get; set; }
        public int PatientId { get; set; }

        public class GetAllADLFunctionQueryHandler : IRequestHandler<GetAllADLFunctionQuery, IDataResult<IEnumerable<GetADLListDto>>>
        {
            private readonly IADLFunctionRepository _aDLFunctionRepository;
            private readonly IADLLookupRepository _aDLLookupRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public GetAllADLFunctionQueryHandler(IADLFunctionRepository aDLFunctionRepository, IADLLookupRepository aDLLookupRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _aDLFunctionRepository = aDLFunctionRepository;
                _aDLLookupRepository = aDLLookupRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<GetADLListDto>>> Handle(GetAllADLFunctionQuery request, CancellationToken cancellationToken)
            {
                var list = await _aDLLookupRepository.GetAllADLFunctionList(request.ProviderId, request.PatientId);

                return new SuccessDataResult<IEnumerable<GetADLListDto>> (list);
            }
        }
    }
}
