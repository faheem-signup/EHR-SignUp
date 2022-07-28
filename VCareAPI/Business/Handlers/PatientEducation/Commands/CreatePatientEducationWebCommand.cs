using AutoMapper;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
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
    public class CreatePatientEducationWebCommand : IRequest<IResult>
    {
        public string Title { get; set; }
        public string WebUrl { get; set; }
        public int PatientId { get; set; }

        public class CreatePatientEducationWebCommandHandler : IRequestHandler<CreatePatientEducationWebCommand, IResult>
        {
            private readonly IPatientEducationWebRepository _patientEducationWebRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            public CreatePatientEducationWebCommandHandler(IPatientEducationWebRepository patientEducationWebRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientEducationWebRepository = patientEducationWebRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientEducationWeb), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientEducationWebCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                PatientEducationWeb patientEducationWebObj = new PatientEducationWeb
                {
                    Title = request.Title,
                    WebUrl = request.WebUrl,
                    PatientId = request.PatientId,
                };

                _patientEducationWebRepository.Add(patientEducationWebObj);
                await _patientEducationWebRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
