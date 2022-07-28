using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientCommunicationRepository;
using Entities.Concrete.PatientCommunicationEntity;
using Entities.Dtos.PatientCommunicationDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientCommunications.Queries
{
    public class GetPatientCommunicationByIdQuery : IRequest<IDataResult<PatientCommunicationDto>>
    {
        public int CommunicationId { get; set; }
        public class GetPatientCommunicationByIdQueryHandler : IRequestHandler<GetPatientCommunicationByIdQuery, IDataResult<PatientCommunicationDto>>
        {
            private readonly IPatientCommunicationRepository _patientCommunicationRepository;

            public GetPatientCommunicationByIdQueryHandler(IPatientCommunicationRepository patientCommunicationRepository)
            {
                _patientCommunicationRepository = patientCommunicationRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<PatientCommunicationDto>> Handle(GetPatientCommunicationByIdQuery request, CancellationToken cancellationToken)
            {
                PatientCommunicationDto patientCom = new PatientCommunicationDto();
                var patientCommunicationobj = await _patientCommunicationRepository.GetAsync(x => x.CommunicationId == request.CommunicationId && x.IsDeleted != true);

                if (patientCommunicationobj != null)
                {
                    patientCom.CommunicationId = patientCommunicationobj.CommunicationId;
                    patientCom.CommunicationDate = patientCommunicationobj.CommunicationDate;
                    patientCom.CommunicationTime = patientCommunicationobj.CommunicationTime;
                    patientCom.CallDetailDescription = patientCommunicationobj.CallDetailDescription;
                    patientCom.PatientId = patientCommunicationobj.PatientId;
                    patientCom.CallDetailTypeId = patientCommunicationobj.CallDetailTypeId;
                    patientCom.CommunicateBy = patientCommunicationobj.CommunicateBy;
                }

                return new SuccessDataResult<PatientCommunicationDto>(patientCom);
            }
        }
    }
}
