using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientVitalsRepository;
using Entities.Concrete.PatientVitalsEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientVital.Commands
{
    public class CreatePatientVitalsCommand : IRequest<IResult>
    {
        public int? ProviderId { get; set; }
        public int PatientId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string Waist { get; set; }
        public string SystolicBP { get; set; }
        public string DiaSystolicBP { get; set; }
        public string HeartRate { get; set; }
        public string RespiratoryRate { get; set; }
        public string Temprature { get; set; }
        public string PainScale { get; set; }
        public string HeadCircumference { get; set; }
        public string Comment { get; set; }

        public class CreatePatientVitalsCommandHandler : IRequestHandler<CreatePatientVitalsCommand, IResult>
        {
            private readonly IPatientVitalsRepository _patientVitalsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientVitalsCommandHandler(IPatientVitalsRepository patientVitalsRepository,IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientVitalsRepository = patientVitalsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientVitals), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientVitalsCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                PatientVitals PatientVitalsObj = new PatientVitals
                {
                    ProviderId = request.ProviderId,
                    PatientId = request.PatientId,
                    VisitDate = request.VisitDate,
                    Height = request.Height,
                    Weight = request.Weight,
                    BMI = request.BMI,
                    Waist = request.Waist,
                    SystolicBP = request.SystolicBP,
                    DiaSystolicBP = request.DiaSystolicBP,
                    HeartRate = request.HeartRate,
                    RespiratoryRate = request.RespiratoryRate,
                    Temprature = request.Temprature,
                    PainScale = request.PainScale,
                    HeadCircumference = request.HeadCircumference,
                    Comment = request.Comment,
                };

                _patientVitalsRepository.Add(PatientVitalsObj);
                await _patientVitalsRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
