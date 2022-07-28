using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEmploymentsRepository;
using DataAccess.Abstract.IPatientInfoDetailsRepository;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using DataAccess.Abstract.IPatientProvidersRepository;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientAdditionalInfoQuery : IRequest<IDataResult<PatientAdditionalInfoDto>>
    {
        public int PatientId { get; set; }

        public class GetPatientAdditionalInfoQueryHandler : IRequestHandler<GetPatientAdditionalInfoQuery, IDataResult<PatientAdditionalInfoDto>>
        {
            private readonly IPatientInfoDetailsRepository _patientInfoDetailsRepository;
            private readonly IPatientEmploymentsRepository _patientEmploymentsRepository;
            private readonly IPatientProvidersRepository _patientProvidersRepository;
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetPatientAdditionalInfoQueryHandler(
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
            }

            public async Task<IDataResult<PatientAdditionalInfoDto>> Handle(GetPatientAdditionalInfoQuery request, CancellationToken cancellationToken)
            {
                PatientAdditionalInfoDto patientAdditionalInfoData = new PatientAdditionalInfoDto();

                var patientEmploymentsUpdateObj = await _patientEmploymentsRepository.GetAsync(x => x.PatientId == request.PatientId);

                if (patientEmploymentsUpdateObj != null)
                {
                    patientAdditionalInfoData.EmploymentStatus = patientEmploymentsUpdateObj.EmploymentStatus;
                    patientAdditionalInfoData.WorkStatus = patientEmploymentsUpdateObj.WorkStatus;
                    patientAdditionalInfoData.EmployerName = patientEmploymentsUpdateObj.EmployerName;
                    patientAdditionalInfoData.EmployerAddress = patientEmploymentsUpdateObj.EmployerAddress;
                    patientAdditionalInfoData.EmployerPhone = patientEmploymentsUpdateObj.EmployerPhone;
                    patientAdditionalInfoData.AccidentDate = patientEmploymentsUpdateObj.AccidentDate;
                    patientAdditionalInfoData.AccidentType = patientEmploymentsUpdateObj.AccidentType;
                    patientAdditionalInfoData.Wc = patientEmploymentsUpdateObj.Wc;

                    var patientEmploymentsDrpNameObj = await _patientEmploymentsRepository.GetPatientEmploymentDropdownName(patientEmploymentsUpdateObj.EmploymentStatus, patientEmploymentsUpdateObj.WorkStatus, patientEmploymentsUpdateObj.AccidentType);

                    if (patientEmploymentsDrpNameObj != null)
                    {
                        patientAdditionalInfoData.EmploymentStatusDescription = patientEmploymentsDrpNameObj.EmploymentStatusDescription;
                        patientAdditionalInfoData.WorkStatusDescription = patientEmploymentsDrpNameObj.WorkStatusDescription;
                        patientAdditionalInfoData.AccidentTypeDescription = patientEmploymentsDrpNameObj.AccidentTypeDescription;
                    }
                }

                var patientInfoDetailsUpdateObj = await _patientInfoDetailsRepository.GetAsync(x => x.PatientId == request.PatientId);
                if (patientInfoDetailsUpdateObj != null)
                {
                    patientAdditionalInfoData.SmokingStatus = patientInfoDetailsUpdateObj.SmokingStatus;
                    patientAdditionalInfoData.Packs = patientInfoDetailsUpdateObj.Packs;
                    patientAdditionalInfoData.HospitalizationStatus = patientInfoDetailsUpdateObj.HospitalizationStatus;
                    patientAdditionalInfoData.LastHospitalizationDate = patientInfoDetailsUpdateObj.LastHospitalizationDate;
                    patientAdditionalInfoData.DisabilityDate = patientInfoDetailsUpdateObj.DisabilityDate;
                    patientAdditionalInfoData.DisabilityStatus = patientInfoDetailsUpdateObj.DisabilityStatus;
                    patientAdditionalInfoData.DeathDate = patientInfoDetailsUpdateObj.DeathDate;
                    patientAdditionalInfoData.DeathReason = patientInfoDetailsUpdateObj.DeathReason;
                    patientAdditionalInfoData.SubstanceAbuseStatus = patientInfoDetailsUpdateObj.SubstanceAbuseStatus;
                    patientAdditionalInfoData.Alcohol = patientInfoDetailsUpdateObj.Alcohol;
                    patientAdditionalInfoData.IllicitSubstances = patientInfoDetailsUpdateObj.IllicitSubstances;

                    var patientInfoDetailsDrpNameObj = await _patientInfoDetailsRepository.GetPatientInfoDetailDropdownName(patientInfoDetailsUpdateObj.SmokingStatus, patientInfoDetailsUpdateObj.Packs, patientInfoDetailsUpdateObj.HospitalizationStatus, patientInfoDetailsUpdateObj.DisabilityStatus, patientInfoDetailsUpdateObj.SubstanceAbuseStatus, patientInfoDetailsUpdateObj.Alcohol, patientInfoDetailsUpdateObj.IllicitSubstances);

                    if (patientInfoDetailsDrpNameObj != null)
                    {
                        patientAdditionalInfoData.SmokingStatusDescription = patientInfoDetailsDrpNameObj.SmokingStatusDescription;
                        patientAdditionalInfoData.PacksDescription = patientInfoDetailsDrpNameObj.PacksDescription;
                        patientAdditionalInfoData.HospitalizationStatusDescription = patientInfoDetailsDrpNameObj.HospitalizationStatusDescription;
                        patientAdditionalInfoData.PatientDisabilityStatusDescription = patientInfoDetailsDrpNameObj.PatientDisabilityStatusDescription;
                        patientAdditionalInfoData.SubstanceAbuseStatusDescription = patientInfoDetailsDrpNameObj.SubstanceAbuseStatusDescription;
                        patientAdditionalInfoData.AlcoholDescription = patientInfoDetailsDrpNameObj.AlcoholDescription;
                        patientAdditionalInfoData.IllicitSubstancesDescription = patientInfoDetailsDrpNameObj.IllicitSubstancesDescription;
                    }
                }

                patientAdditionalInfoData.PatientId = request.PatientId;

                var patientProviderUpdateObj = await _patientProvidersRepository.GetAsync(x => x.PatientId == request.PatientId);
                if (patientProviderUpdateObj != null)
                {
                    patientAdditionalInfoData.AttendingPhysician = patientProviderUpdateObj.AttendingPhysician;
                    patientAdditionalInfoData.SupervisingProvider = patientProviderUpdateObj.SupervisingProvider;
                    patientAdditionalInfoData.LocationId = patientProviderUpdateObj.LocationId;
                    patientAdditionalInfoData.PatientId = patientProviderUpdateObj.PatientId;

                    var patientProviderDrpNameObj = await _patientProvidersRepository.GetPatientProviderDropdownName(patientProviderUpdateObj.LocationId);

                    if (patientProviderDrpNameObj != null)
                    {
                        patientAdditionalInfoData.LocationName = patientProviderDrpNameObj.LocationName;
                    }
                }

                var referralProviderList = await _patientProvideReferralRepository.GetPatientProviderReferringList(request.PatientId);

                if (referralProviderList.Count() > 0)
                {
                    patientAdditionalInfoData._patientProvideReferring = referralProviderList;
                }

                return new SuccessDataResult<PatientAdditionalInfoDto>(patientAdditionalInfoData);
            }
        }
    }
}
