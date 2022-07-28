using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IICDToPracticesRepository;
using Entities.Concrete.ICDToPracticesEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ICDToPractice.Commands
{

    public class CreateICDToPracticesCommand : IRequest<IResult>
    {
        public int PracticeId { get; set; }
        public int[] ICDToPracticesIds { get; set; }

        public class CreateICDToPracticesCommandHandler : IRequestHandler<CreateICDToPracticesCommand, IResult>
        {
            private readonly IICDToPracticesRepository _icdToPracticesRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateICDToPracticesCommandHandler(IICDToPracticesRepository icdToPracticesRepository, IMediator mediator, IMapper mapper)
            {
                _icdToPracticesRepository = icdToPracticesRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [ValidationAspect(typeof(ValidatorICDToPractice), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateICDToPracticesCommand request, CancellationToken cancellationToken)
            {
                var existingList = await _icdToPracticesRepository.GetListAsync(x => x.PracticeId == request.PracticeId);
                if (request.ICDToPracticesIds.Count() > 0)
                {
                    var icdToPracticesList = request.ICDToPracticesIds.Select(x => new ICDToPractices() { ICDCategoryId = x, PracticeId = request.PracticeId});
                    _icdToPracticesRepository.BulkInsert(existingList, icdToPracticesList);
                    await _icdToPracticesRepository.SaveChangesAsync();
                }
                else
                {
                    return new ErrorResult("Please Select At Least One Practice ICD Group");
                }

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

