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
    public class UpdatePatientVitalsCommand : IRequest<IResult>
    {
        public int PatientVitalsId { get; set; }
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

        public class UpdatePatientVitalsCommandHandler : IRequestHandler<UpdatePatientVitalsCommand, IResult>
        {
            private readonly IPatientVitalsRepository _PatientVitalsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdatePatientVitalsCommandHandler(IPatientVitalsRepository PatientVitalsRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _PatientVitalsRepository = PatientVitalsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientVitals), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientVitalsCommand request, CancellationToken cancellationToken)
            {
                var PatientVitalsUpdateObj = await _PatientVitalsRepository.GetAsync(x => x.PatientVitalsId == request.PatientVitalsId);
                if(PatientVitalsUpdateObj != null)
                {
                    PatientVitalsUpdateObj.PatientVitalsId = request.PatientVitalsId;
                    PatientVitalsUpdateObj.PatientId = request.PatientId;
                    PatientVitalsUpdateObj.ProviderId = request.ProviderId;
                    PatientVitalsUpdateObj.VisitDate = request.VisitDate;
                    PatientVitalsUpdateObj.Height = request.Height;
                    PatientVitalsUpdateObj.Weight = request.Weight;
                    PatientVitalsUpdateObj.BMI = request.BMI;
                    PatientVitalsUpdateObj.Waist = request.Waist;
                    PatientVitalsUpdateObj.SystolicBP = request.SystolicBP;
                    PatientVitalsUpdateObj.DiaSystolicBP = request.DiaSystolicBP;
                    PatientVitalsUpdateObj.HeartRate = request.HeartRate;
                    PatientVitalsUpdateObj.RespiratoryRate = request.RespiratoryRate;
                    PatientVitalsUpdateObj.Temprature = request.Temprature;
                    PatientVitalsUpdateObj.PainScale = request.PainScale;
                    PatientVitalsUpdateObj.HeadCircumference = request.HeadCircumference;
                    PatientVitalsUpdateObj.Comment = request.Comment;

                    _PatientVitalsRepository.Update(PatientVitalsUpdateObj);
                    await _PatientVitalsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
