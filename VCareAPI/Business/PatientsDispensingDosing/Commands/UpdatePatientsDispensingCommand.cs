using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingDosingRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensingDosing.Commands
{
    public class UpdatePatientDispensingDosingCommand : IRequest<IResult>
    {

        public int DispensingDosingId { get; set; }
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

        public class UpdatePatientDispensingDosingCommandHandler : IRequestHandler<UpdatePatientDispensingDosingCommand, IResult>
        {
            private readonly IPatientDispensingDosingRepository _patientDispensingDosingRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdatePatientDispensingDosingCommandHandler(IPatientDispensingDosingRepository patientDispensingDosingRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDispensingDosingRepository = patientDispensingDosingRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientDispensingDosing), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientDispensingDosingCommand request, CancellationToken cancellationToken)
            {
                var patientDispensingDosingUpdateObj = await _patientDispensingDosingRepository.GetAsync(x => x.DispensingDosingId == request.DispensingDosingId);
                if(patientDispensingDosingUpdateObj != null)
                {
                    patientDispensingDosingUpdateObj.PatientId = request.PatientId;
                    patientDispensingDosingUpdateObj.ProgramId = request.ProgramId;
                    patientDispensingDosingUpdateObj.StartDate = request.StartDate;
                    patientDispensingDosingUpdateObj.EndDate = request.EndDate;
                    patientDispensingDosingUpdateObj.TherapistId = request.TherapistId;
                    patientDispensingDosingUpdateObj.LastMidVisit = request.LastMidVisit;
                    patientDispensingDosingUpdateObj.LastDose = request.LastDose;
                    patientDispensingDosingUpdateObj.TakeHome = request.TakeHome;
                    patientDispensingDosingUpdateObj.MedicatedThru = request.MedicatedThru;
                    patientDispensingDosingUpdateObj.MedThruDose = request.MedThruDose;
                    patientDispensingDosingUpdateObj.LevelOfCare = request.LevelOfCare;
                    patientDispensingDosingUpdateObj.LastUAResult = request.LastUAResult;
                    patientDispensingDosingUpdateObj.Status = request.Status;
                    patientDispensingDosingUpdateObj.MedicationId = request.MedicationId;
                    patientDispensingDosingUpdateObj.ScheduleId = request.ScheduleId;

                    _patientDispensingDosingRepository.Update(patientDispensingDosingUpdateObj);
                    await _patientDispensingDosingRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
