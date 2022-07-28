using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingRepository;
using Entities.Concrete.PatientDispensingEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensing.Commands
{
    public class CreatePatientDispensingCommand : IRequest<IResult>
    {
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
        public class CreatePatientDispensingCommandHandler : IRequestHandler<CreatePatientDispensingCommand, IResult>
        {
            private readonly IPatientDispensingRepository _patientDispensingRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreatePatientDispensingCommandHandler(IPatientDispensingRepository patientDispensingRepository,IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _patientDispensingRepository = patientDispensingRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorPatientDispensing), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientDispensingCommand request, CancellationToken cancellationToken)
            {
               // var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                PatientDispensing patientDispensingObj = new PatientDispensing
                {
                    PatientId = request.PatientId,
                    ProgramId = request.ProgramId,
                    DrugUsage = request.DrugUsage,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    MainBottle = request.MainBottle,
                    TotalDispensed = request.TotalDispensed,
                    TotalDispensedUnitId = request.TotalDispensedUnitId,
                    Remaining = request.Remaining,
                    RemainingUnitId = request.RemainingUnitId,
                    TotalQuantity = request.TotalQuantity,
                    TotalQuantityUnitId = request.TotalQuantityUnitId,
                };

                _patientDispensingRepository.Add(patientDispensingObj);
                await _patientDispensingRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
