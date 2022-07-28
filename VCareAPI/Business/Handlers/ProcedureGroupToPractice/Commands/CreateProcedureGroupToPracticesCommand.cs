using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IProcedureGroupToPracticesRepository;
using Entities.Concrete.ProcedureGroupToPracticesEntity;
using Entities.Dtos.ProcedureGroupsToPractice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ProcedureGroupToPractice.Commands
{

    public class CreateProcedureGroupToPracticesCommand : IRequest<IResult>
    {
        public int PracticeId { get; set; }
        public List<SaveProcedureGroupToPracticesDto> ProcedureGroupToPracticeList { get; set; }

        public class CreateProcedureGroupToPracticesCommandHandler : IRequestHandler<CreateProcedureGroupToPracticesCommand, IResult>
        {
            private readonly IProcedureGroupToPracticesRepository _procedureGroupToPracticesRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateProcedureGroupToPracticesCommandHandler(IProcedureGroupToPracticesRepository procedureGroupToPracticesRepository, IMediator mediator, IMapper mapper)
            {
                _procedureGroupToPracticesRepository = procedureGroupToPracticesRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [ValidationAspect(typeof(ValidatorProcedureGroupToPractice), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProcedureGroupToPracticesCommand request, CancellationToken cancellationToken)
            {
                var existingList = await _procedureGroupToPracticesRepository.GetListAsync(x => x.PracticeId == request.PracticeId);

                if (request.ProcedureGroupToPracticeList.Count() > 0)
                {

                    var procedureGroupToPracticesList = request.ProcedureGroupToPracticeList.Select(x => new ProcedureGroupToPractices() { ProcedureGroupId = x.ProcedureGroupId, ProcedureSubGroupId = x.ProcedureSubGroupId, PracticeId = request.PracticeId});
                    _procedureGroupToPracticesRepository.BulkInsert(existingList, procedureGroupToPracticesList);
                    await _procedureGroupToPracticesRepository.SaveChangesAsync();
                }
                else
                {
                    return new ErrorResult("Please Select At Least One Practice CPT Group");
                }
                return new SuccessResult(Messages.Added);
            }
        }
    }
}

