using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ADLFunctionRepository;
using Entities.Concrete.ADLEntity;
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
    public class CreateADLFunctionCommand : IRequest<IResult>
    {
        public int ADLCategoryId { get; set; }
        public string ADLFunctionName { get; set; }
        public class CreateADLFunctionCommandHandler : IRequestHandler<CreateADLFunctionCommand, IResult>
        {
            private readonly IADLFunctionRepository _aDLFunctionRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateADLFunctionCommandHandler(IADLFunctionRepository aDLFunctionRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _aDLFunctionRepository = aDLFunctionRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateADLFunctionCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                ADLFunction aDLFunctionObj = new ADLFunction
                {
                    ADLCategoryId = request.ADLCategoryId,
                    ADLFunctionName = request.ADLFunctionName,
                };

                _aDLFunctionRepository.Add(aDLFunctionObj);
                await _aDLFunctionRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
