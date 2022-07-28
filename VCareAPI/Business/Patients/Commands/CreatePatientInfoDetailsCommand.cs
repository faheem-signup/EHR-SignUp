using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEmploymentsRepository;
using DataAccess.Abstract.IPatientInfoDetailsRepository;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using DataAccess.Abstract.IPatientProvidersRepository;
using Entities.Concrete.PatientEmploymentEntity;
using Entities.Concrete.PatientInfoDetailsEntity;
using Entities.Concrete.PatientProviderEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Commands
{
    public class CreatePatientInfoDetailsCommand : IRequest<IResult>
    {
        public int? PatientId { get; set; }
        //MORE INFO
        public int? SmokingStatus { get; set; }
        public int? Packs { get; set; }
        public int? HospitalizationStatus { get; set; }
        public DateTime? LastHospitalizationDate { get; set; }
        public DateTime? DisabilityDate { get; set; }
        public int? DisabilityStatus { get; set; }
        public DateTime? DeathDate { get; set; }
        public string DeathReason { get; set; }
        public int? SubstanceAbuseStatus { get; set; }
        public int? Alcohol { get; set; }
        public int? IllicitSubstances { get; set; }
        //ACCIDENT DETAILS
        public int? EmploymentStatus { get; set; }
        public int? WorkStatus { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerPhone { get; set; }
        public DateTime? AccidentDate { get; set; }
        public int? AccidentType { get; set; }
        public string Wc { get; set; }
        //PROVIDER DETAILS
        public string AttendingPhysician { get; set; }
        public string SupervisingProvider { get; set; }
        public int? LocationId { get; set; }
        //public int[] ReferralProviderId { get; set; }
        //public int? ReferringProvider { get; set; }
        //public int? PCPName { get; set; }
        //public string Pharmacy { get; set; }
        //public int? ReferringAgency { get; set; }
        //public int? DrugsAgency { get; set; }
        //public int? ProbationOfficer { get; set; }
        
        public class CreatePatientInfoDetailsCommandHandler : IRequestHandler<CreatePatientInfoDetailsCommand, IResult>
        {
            private readonly IPatientInfoDetailsRepository _patientInfoDetailsRepository;
            private readonly IPatientEmploymentsRepository _patientEmploymentsRepository;
            private readonly IPatientProvidersRepository _patientProvidersRepository;
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreatePatientInfoDetailsCommandHandler(
                IPatientInfoDetailsRepository patientInfoDetailsRepository,
                IPatientEmploymentsRepository patientEmploymentsRepository,
                IPatientProvidersRepository patientProvidersRepository,
                IPatientProvideReferralRepository patientProvideReferralRepository,
                IMediator mediator,
                IMapper mapper)
            {
                _patientInfoDetailsRepository = patientInfoDetailsRepository;
                _patientEmploymentsRepository = patientEmploymentsRepository;
                _patientProvidersRepository = patientProvidersRepository;
                _patientProvideReferralRepository = patientProvideReferralRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(ValidatorCreatePatientInfoDetails), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreatePatientInfoDetailsCommand request, CancellationToken cancellationToken)
            {
                PatientProvider patientProviderObj = new PatientProvider
                {
                    AttendingPhysician = request.AttendingPhysician,
                    SupervisingProvider = request.SupervisingProvider,
                    LocationId = request.LocationId,
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

                //if (request.ReferralProviderId != null && request.ReferralProviderId.Length > 0)
                //{
                //    var currentReferralProviderList = request.ReferralProviderId.Select(x => new PatientProvideReferring()
                //    {
                //        ReferralProviderId = x,
                //        PatientProviderId = patientProviderObj.PatientProviderId,
                //    });

                //    var existingReferralProviderList = await _patientProvideReferralRepository.GetListAsync(x => x.PatientProviderId == patientProviderObj.PatientProviderId);
                //    _patientProvideReferralRepository.BulkInsert(existingReferralProviderList, currentReferralProviderList);
                //    await _patientProvideReferralRepository.SaveChangesAsync();
                //}

                PatientEmployment patientEmploymentObj = new PatientEmployment
                {
                    EmploymentStatus = request.EmploymentStatus,
                    WorkStatus = request.WorkStatus,
                    EmployerName = request.EmployerName,
                    EmployerAddress = request.EmployerAddress,
                    EmployerPhone = request.EmployerPhone,
                    AccidentDate = request.AccidentDate,
                    AccidentType = request.AccidentType,
                    Wc = request.Wc,
                    PatientId = request.PatientId
                };

                _patientEmploymentsRepository.Add(patientEmploymentObj);
                await _patientEmploymentsRepository.SaveChangesAsync();

                PatientInfoDetail patientInfoDetailObj = new PatientInfoDetail
                {
                    SmokingStatus = request.SmokingStatus,
                    Packs = request.Packs,
                    HospitalizationStatus = request.HospitalizationStatus,
                    LastHospitalizationDate = request.LastHospitalizationDate,
                    DisabilityDate = request.DisabilityDate,
                    DisabilityStatus = request.DisabilityStatus,
                    DeathDate = request.DeathDate,
                    DeathReason = request.DeathReason,
                    PatientId = request.PatientId,
                    SubstanceAbuseStatus = request.SubstanceAbuseStatus,
                    Alcohol = request.Alcohol,
                    IllicitSubstances = request.IllicitSubstances,
                };

                _patientInfoDetailsRepository.Add(patientInfoDetailObj);
                await _patientInfoDetailsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
