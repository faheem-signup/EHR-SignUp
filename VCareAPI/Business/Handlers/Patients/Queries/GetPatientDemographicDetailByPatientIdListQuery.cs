using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IFormTemplateRepository;
using DataAccess.Abstract.IPatientCommunicationRepository;
using DataAccess.Abstract.IPatientEducationDocumentRepository;
using DataAccess.Abstract.IPatientInfoDetailsRepository;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using DataAccess.Abstract.IPatientProvidersRepository;
using DataAccess.Abstract.IPatientRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientDemographicDetailByPatientIdListQuery : IRequest<IDataResult<PatientDemographicDTo>>// IRequest<IDataResult<IEnumerable<Appointment>>>
    {
        public int PatientId { get; set; }
        public class GetPatientDemographicDetailByPatientIdListQueryHandler : IRequestHandler<GetPatientDemographicDetailByPatientIdListQuery, IDataResult<PatientDemographicDTo>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;
            private readonly IPatientEducationDocumentRepository _patientEducationDocumentRepository;
            private readonly IFormTemplateRepository _formTemplateRepository;

            public GetPatientDemographicDetailByPatientIdListQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper, IPatientRepository patientRepository, IPatientCommunicationRepository patientCommunicationRepository,
                IPatientEducationDocumentRepository patientEducationDocumentRepository, IFormTemplateRepository formTemplateRepository)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
                _patientRepository = patientRepository;
                _patientCommunicationRepository = patientCommunicationRepository;
                _patientEducationDocumentRepository = patientEducationDocumentRepository;
                _formTemplateRepository = formTemplateRepository;
            }

            //  [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            // [CacheAspect(10)]
            public async Task<IDataResult<PatientDemographicDTo>> Handle(GetPatientDemographicDetailByPatientIdListQuery request, CancellationToken cancellationToken)
            {
                PatientDemographicDTo patientDemographicObj = new PatientDemographicDTo();

                patientDemographicObj.PatientDetail = await _patientRepository.GetPatientDetailById(request.PatientId);
                patientDemographicObj.PatientAdditionalInfoDetail = await _patientRepository.GetPatientProvidersDetailById(request.PatientId);
                var patientInfoDetailObj = await _patientRepository.GetPatientInfoDetailById(request.PatientId);
                if (patientInfoDetailObj != null)
                {
                    patientDemographicObj.PatientAdditionalInfoDetail.SubstanceAbuseStatusName = patientInfoDetailObj.SubstanceAbuseStatusName;
                    patientDemographicObj.PatientAdditionalInfoDetail.IllicitSubstancesName = patientInfoDetailObj.IllicitSubstancesName;
                    patientDemographicObj.PatientAdditionalInfoDetail.AlcoholName = patientInfoDetailObj.AlcoholName;
                }

                patientDemographicObj.PatientVitalsDetail = await _patientRepository.GetPatientVitalDetailById(request.PatientId);

                patientDemographicObj.PatientDiagnosisCodeDetail = await _patientRepository.GetPatientDiagnosisCodeDetailById(request.PatientId);

                patientDemographicObj.PatientInsuranceList = await _patientRepository.GetPatientInsurancesDetailById(request.PatientId);

                patientDemographicObj.PatientCommunicationList = await _patientCommunicationRepository.GetPatientCommunication(request.PatientId);

                patientDemographicObj.PatientDocumentList= await _patientRepository.GetPatientDocumentById(request.PatientId);

                patientDemographicObj.PatientEducationDocumentList =await _patientEducationDocumentRepository.GetListAsync(x => x.PatientId == request.PatientId);

                patientDemographicObj.PatientNotesList =await _formTemplateRepository.GetNotes(request.PatientId,0);

                patientDemographicObj.AppointmentDetail = await _appointmentRepository.GetAppointemntDetailByPatientId(request.PatientId);


                return new SuccessDataResult<PatientDemographicDTo>(patientDemographicObj);

            }
        }
    }
}
