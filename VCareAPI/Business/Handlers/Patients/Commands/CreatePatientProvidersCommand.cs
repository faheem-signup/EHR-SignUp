using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientProvidersRepository;
using Entities.Concrete.PatientProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Commands
{
    public class CreatePatientProvidersCommand : IRequest<IResult>
    {
        public string AttendingPhysician { get; set; }
        public string SupervisingProvider { get; set; }
        public int? ReferringProvider { get; set; }
        public int? PCPName { get; set; }
        public string Pharmacy { get; set; }
        public int? ReferringAgency { get; set; }
        public int? DrugsAgency { get; set; }
        public int? ProbationOfficer { get; set; }
        public int? PatientId { get; set; }
        public class CreatePatientProvidersCommandHandler : IRequestHandler<CreatePatientProvidersCommand, IResult>
        {
            private readonly IPatientProvidersRepository _patientProvidersRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreatePatientProvidersCommandHandler(IPatientProvidersRepository patientProvidersRepository, IMediator mediator, IMapper mapper)
            {
                _patientProvidersRepository = patientProvidersRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientProvidersCommand request, CancellationToken cancellationToken)
            {
                PatientProvider patientProviderObj = new PatientProvider
                {
                    AttendingPhysician = request.AttendingPhysician,
                    SupervisingProvider = request.SupervisingProvider,
                    //ReferringProvider = request.ReferringProvider,
                    //PCPName = request.PCPName,
                    //Pharmacy = request.Pharmacy,
                    //ReferringAgency = request.ReferringAgency,
                    //DrugsAgency = request.DrugsAgency,
                    //ProbationOfficer = request.ProbationOfficer,
                    PatientId = request.PatientId,
                };

                _patientProvidersRepository.Add(patientProviderObj);
                await _patientProvidersRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
