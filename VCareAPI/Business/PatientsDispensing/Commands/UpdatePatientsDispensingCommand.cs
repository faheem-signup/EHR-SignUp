using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensing.Commands
{
    public class UpdatePatientDispensingCommand : IRequest<IResult>
    {
        public int DispensingId { get; set; }
        public int PatientId { get; set; }
        public int ProgramId { get; set; }
        public string DrugUsage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MainBottle { get; set; }
        public decimal? TotalDispensed { get; set; }
        public int? TotalDispensedUnitId { get; set; }
        public decimal? Remaining { get; set; }
        public int? RemainingUnitId { get; set; }
        public decimal? TotalQuantity { get; set; }
        public int? TotalQuantityUnitId { get; set; }

        public class UpdatePatientDispensingCommandHandler : IRequestHandler<UpdatePatientDispensingCommand, IResult>
        {
            private readonly IPatientDispensingRepository _patientDispensingRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdatePatientDispensingCommandHandler(IPatientDispensingRepository patientDispensingRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDispensingRepository = patientDispensingRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdatePatientDispensing), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientDispensingCommand request, CancellationToken cancellationToken)
            {
                var patientDispensingUpdateObj = await _patientDispensingRepository.GetAsync(x => x.DispensingId == request.DispensingId);
                if (patientDispensingUpdateObj != null)
                {
                    patientDispensingUpdateObj.PatientId = request.PatientId;
                    patientDispensingUpdateObj.ProgramId = request.ProgramId;
                    patientDispensingUpdateObj.DrugUsage = request.DrugUsage;
                    patientDispensingUpdateObj.StartDate = request.StartDate;
                    patientDispensingUpdateObj.EndDate = request.EndDate;
                    patientDispensingUpdateObj.MainBottle = request.MainBottle;
                    patientDispensingUpdateObj.TotalDispensed = request.TotalDispensed;
                    patientDispensingUpdateObj.TotalDispensedUnitId = request.TotalDispensedUnitId;
                    patientDispensingUpdateObj.Remaining = request.Remaining;
                    patientDispensingUpdateObj.RemainingUnitId = request.RemainingUnitId;
                    patientDispensingUpdateObj.TotalQuantity = request.TotalQuantity;
                    patientDispensingUpdateObj.TotalQuantityUnitId = request.TotalQuantityUnitId;

                    _patientDispensingRepository.Update(patientDispensingUpdateObj);
                    await _patientDispensingRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Updated);
                }

                return new SuccessResult(Messages.NotUpdated);
            }
        }
    }
}
