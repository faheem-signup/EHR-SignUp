using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEducationWebRepository;
using Entities.Concrete.PatientEducationEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientEducation.Commands
{
    public class DeletePatientEducationWebCommand : IRequest<IResult>
    {
        public int PatientEducationWebId { get; set; }
        public class DeletePatientEducationWebCommandHandler : IRequestHandler<DeletePatientEducationWebCommand, IResult>
        {
            private readonly IPatientEducationWebRepository _patientEducationWebRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public DeletePatientEducationWebCommandHandler(IPatientEducationWebRepository patientEducationWebRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientEducationWebRepository = patientEducationWebRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientEducationWebCommand request, CancellationToken cancellationToken)
            {
                var patientEducationWebObj = await _patientEducationWebRepository.GetAsync(x => x.PatientEducationWebId == request.PatientEducationWebId);

                _patientEducationWebRepository.Delete(patientEducationWebObj);
                await _patientEducationWebRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
