using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDiagnosisCodeRepository;
using Entities.Concrete.PatientDiagnosisCodeEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDiagnosisCodes.Commands
{
    public class CreatePatientDiagnosisCodeCommand : IRequest<IResult>
    {
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public int DiagnosisId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime ResolvedDate { get; set; }
        public string DiagnoseCodeType { get; set; }
        public string SNOMEDCode { get; set; }
        public string SNOMEDCodeDesctiption { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public class CreatePatientDiagnosisCodeCommandHandler : IRequestHandler<CreatePatientDiagnosisCodeCommand, IResult>
        {
            private readonly IPatientDiagnosisCodeRepository _patientDiagnosisCodeRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientDiagnosisCodeCommandHandler(IPatientDiagnosisCodeRepository patientDiagnosisCodeRepository,IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDiagnosisCodeRepository = patientDiagnosisCodeRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientDiagnosisCodes), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientDiagnosisCodeCommand request, CancellationToken cancellationToken)
            {
                PatientDiagnosisCode PatientDiagnosisCodeObj = new PatientDiagnosisCode
                {
                    ProviderId = request.ProviderId,
                    PatientId = request.PatientId,
                    DiagnosisId = request.DiagnosisId,
                    DiagnosisDate = request.DiagnosisDate,
                    ResolvedDate = request.ResolvedDate,
                    DiagnoseCodeType = request.DiagnoseCodeType,
                    SNOMEDCode = request.SNOMEDCode,
                    SNOMEDCodeDesctiption = request.SNOMEDCodeDesctiption,
                    Description = request.Description,
                    Comments = request.Comments,
                };

                _patientDiagnosisCodeRepository.Add(PatientDiagnosisCodeObj);
                await _patientDiagnosisCodeRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
