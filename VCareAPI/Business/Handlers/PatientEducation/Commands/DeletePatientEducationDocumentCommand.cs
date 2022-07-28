using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEducationDocumentRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientEducation.Commands
{
    public class DeletePatientEducationDocumentCommand : IRequest<IResult>
    {
        public int PatientEducationDocumentId { get; set; }
        public class DeletePatientEducationDocumentCommandHandler : IRequestHandler<DeletePatientEducationDocumentCommand, IResult>
        {
            private readonly IPatientEducationDocumentRepository _patientEducationDocumentRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public DeletePatientEducationDocumentCommandHandler(IPatientEducationDocumentRepository patientEducationDocumentRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientEducationDocumentRepository = patientEducationDocumentRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientEducationDocumentCommand request, CancellationToken cancellationToken)
            {
                var patientEducationWebObj = await _patientEducationDocumentRepository.GetAsync(x => x.PatientEducationDocumentId == request.PatientEducationDocumentId);

                _patientEducationDocumentRepository.Delete(patientEducationWebObj);
                await _patientEducationDocumentRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
