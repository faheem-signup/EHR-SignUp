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

namespace Business.Handlers.ADLFunctions.Commands
{
    public class UpsertADLFunctionCommand : IRequest<IResult>
    {
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public List<ADLFunctionDto> ADLFunctionList { get; set; }

        public class UpsertADLFunctionCommandHandler : IRequestHandler<UpsertADLFunctionCommand, IResult>
        {
            private readonly IADLFunctionRepository _aDLFunctionRepository;
            private readonly IADLLookupRepository _aDLLookupRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpsertADLFunctionCommandHandler(IADLFunctionRepository aDLFunctionRepository, IADLLookupRepository aDLLookupRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _aDLFunctionRepository = aDLFunctionRepository;
                _aDLLookupRepository = aDLLookupRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpsertADLFunctionCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (request.ADLFunctionList.Count > 0)
                {
                    foreach (var item in request.ADLFunctionList)
                    {
                        var AddADLFunctionObj = await _aDLLookupRepository.GetAsync(x => x.ADLFunctionId == item.ADLFunctionId && x.ProviderId == request.ProviderId && x.PatientId == request.PatientId);
                        if (AddADLFunctionObj == null)
                        {
                            ADLLookup aDLFunctionObj = new ADLLookup
                            {
                                ADLFunctionId = (int)item.ADLFunctionId,
                                Independent = item.Independent,
                                NeedsHelp = item.NeedsHelp,
                                Dependent = item.Dependent,
                                CannotDo = item.CannotDo,
                                ProviderId = request.ProviderId,
                                PatientId = request.PatientId,
                            };

                            _aDLLookupRepository.Add(aDLFunctionObj);
                            await _aDLLookupRepository.SaveChangesAsync();
                        }
                        else
                        {
                            AddADLFunctionObj.ADLFunctionId = (int)item.ADLFunctionId;
                            AddADLFunctionObj.Independent = item.Independent;
                            AddADLFunctionObj.NeedsHelp = item.NeedsHelp;
                            AddADLFunctionObj.Dependent = item.Dependent;
                            AddADLFunctionObj.CannotDo = item.CannotDo;
                            AddADLFunctionObj.ProviderId = request.ProviderId;
                            AddADLFunctionObj.PatientId = request.PatientId;

                            _aDLLookupRepository.Update(AddADLFunctionObj);
                            await _aDLLookupRepository.SaveChangesAsync();
                        }
                    }

                    return new SuccessResult(Messages.Added);
                }

                return new SuccessResult(Messages.NoRecordFound);
            }
        }
    }
}
