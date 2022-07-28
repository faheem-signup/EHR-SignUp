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
    public class UpdatePatientAdditionalInfoCommand : IRequest<IResult>
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

        public class UpdatePatientAdditionalInfoCommandHandler : IRequestHandler<UpdatePatientAdditionalInfoCommand, IResult>
        {
            private readonly IPatientInfoDetailsRepository _patientInfoDetailsRepository;
            private readonly IPatientEmploymentsRepository _patientEmploymentsRepository;
            private readonly IPatientProvidersRepository _patientProvidersRepository;
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public UpdatePatientAdditionalInfoCommandHandler(
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

            [ValidationAspect(typeof(ValidatorUpdatePatientAdditionalInfo), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdatePatientAdditionalInfoCommand request, CancellationToken cancellationToken)
            {

                var patientProviderUpdateObj = await _patientProvidersRepository.GetAsync(x => x.PatientId == request.PatientId);

                if (patientProviderUpdateObj != null)
                {
                    patientProviderUpdateObj.AttendingPhysician = request.AttendingPhysician;
                    patientProviderUpdateObj.SupervisingProvider = request.SupervisingProvider;
                    patientProviderUpdateObj.LocationId = request.LocationId;
                    patientProviderUpdateObj.PatientId = request.PatientId;

                    _patientProvidersRepository.Update(patientProviderUpdateObj);
                    await _patientProvidersRepository.SaveChangesAsync();
                }
                else
                {
                    PatientProvider patientProviderObj = new PatientProvider
                    {
                        AttendingPhysician = request.AttendingPhysician,
                        SupervisingProvider = request.SupervisingProvider,
                        LocationId = request.LocationId,
                        PatientId = request.PatientId,
                    };

                    _patientProvidersRepository.Add(patientProviderObj);
                    await _patientProvidersRepository.SaveChangesAsync();
                }

                var patientEmploymentsUpdateObj = await _patientEmploymentsRepository.GetAsync(x => x.PatientId == request.PatientId);

                if (patientEmploymentsUpdateObj != null)
                {
                    patientEmploymentsUpdateObj.EmploymentStatus = request.EmploymentStatus;
                    patientEmploymentsUpdateObj.WorkStatus = request.WorkStatus;
                    patientEmploymentsUpdateObj.EmployerName = request.EmployerName;
                    patientEmploymentsUpdateObj.EmployerAddress = request.EmployerAddress;
                    patientEmploymentsUpdateObj.EmployerPhone = request.EmployerPhone;
                    patientEmploymentsUpdateObj.AccidentDate = request.AccidentDate;
                    patientEmploymentsUpdateObj.AccidentType = request.AccidentType;
                    patientEmploymentsUpdateObj.Wc = request.Wc;
                    patientEmploymentsUpdateObj.PatientId = request.PatientId;

                    _patientEmploymentsRepository.Update(patientEmploymentsUpdateObj);
                    await _patientEmploymentsRepository.SaveChangesAsync();
                }
                else
                {
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
                }

                var patientInfoDetailsUpdateObj = await _patientInfoDetailsRepository.GetAsync(x => x.PatientId == request.PatientId);
                if (patientInfoDetailsUpdateObj != null)
                {
                    patientInfoDetailsUpdateObj.SmokingStatus = request.SmokingStatus;
                    patientInfoDetailsUpdateObj.Packs = request.Packs;
                    patientInfoDetailsUpdateObj.HospitalizationStatus = request.HospitalizationStatus;
                    patientInfoDetailsUpdateObj.LastHospitalizationDate = request.LastHospitalizationDate;
                    patientInfoDetailsUpdateObj.DisabilityDate = request.DisabilityDate;
                    patientInfoDetailsUpdateObj.DisabilityStatus = request.DisabilityStatus;
                    patientInfoDetailsUpdateObj.DeathDate = request.DeathDate;
                    patientInfoDetailsUpdateObj.DeathReason = request.DeathReason;
                    patientInfoDetailsUpdateObj.PatientId = request.PatientId;
                    patientInfoDetailsUpdateObj.Alcohol = request.Alcohol;
                    patientInfoDetailsUpdateObj.Alcohol = request.Alcohol;
                    patientInfoDetailsUpdateObj.SubstanceAbuseStatus = request.SubstanceAbuseStatus;

                    _patientInfoDetailsRepository.Update(patientInfoDetailsUpdateObj);
                    await _patientInfoDetailsRepository.SaveChangesAsync();
                }
                else
                {
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
                        Alcohol = request.Alcohol,
                        IllicitSubstances = request.Alcohol,
                        SubstanceAbuseStatus = request.SubstanceAbuseStatus,
                    };

                    _patientInfoDetailsRepository.Add(patientInfoDetailObj);
                    await _patientInfoDetailsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
