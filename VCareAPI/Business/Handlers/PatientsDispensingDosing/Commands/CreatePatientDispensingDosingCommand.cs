using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingDosingRepository;
using Entities.Concrete.PatientDispensingDosingEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensingDosing.Commands
{
    public class CreatePatientDispensingDosingCommand : IRequest<IResult>
    {
        public int PatientId { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TherapistId { get; set; }
        public string LastMidVisit { get; set; }
        public string LastDose { get; set; }
        public string TakeHome { get; set; }
        public string MedicatedThru { get; set; }
        public string MedThruDose { get; set; }
        public string LevelOfCare { get; set; }
        public string LastUAResult { get; set; }
        public int Status { get; set; }
        public int MedicationId { get; set; }
        public int ScheduleId { get; set; }
        public class CreatePatientDispensingDosingCommandHandler : IRequestHandler<CreatePatientDispensingDosingCommand, IResult>
        {
            private readonly IPatientDispensingDosingRepository _patientDispensingDosingRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientDispensingDosingCommandHandler(IPatientDispensingDosingRepository patientDispensingDosingRepository,IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDispensingDosingRepository = patientDispensingDosingRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientDispensingDosing), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientDispensingDosingCommand request, CancellationToken cancellationToken)
            {
               // var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                PatientDispensingDosing patientDispensingDosingObj = new PatientDispensingDosing
                {
                    PatientId = request.PatientId,
                    ProgramId = request.ProgramId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    TherapistId = request.TherapistId,
                    LastMidVisit = request.LastMidVisit,
                    LastDose = request.LastDose,
                    TakeHome = request.TakeHome,
                    MedicatedThru = request.MedicatedThru,
                    MedThruDose = request.MedThruDose,
                    LevelOfCare = request.LevelOfCare,
                    LastUAResult = request.LastUAResult,
                    Status = request.Status,
                    MedicationId = request.MedicationId,
                    ScheduleId = request.ScheduleId,
                };

                _patientDispensingDosingRepository.Add(patientDispensingDosingObj);
                await _patientDispensingDosingRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
