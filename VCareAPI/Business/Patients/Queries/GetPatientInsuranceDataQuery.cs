using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientInsuranceDataQuery : IRequest<IDataResult<PatientInsuranceDataDto>>
    {
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int? AppointmentId { get; set; }

        public class GetPatientInsuranceDataQueryHandler : IRequestHandler<GetPatientInsuranceDataQuery, IDataResult<PatientInsuranceDataDto>>
        {
            private readonly IPatientRepository _patientRepository;
            public GetPatientInsuranceDataQueryHandler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<PatientInsuranceDataDto>> Handle(GetPatientInsuranceDataQuery request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetPatientInsuranceDataById(request.PatientId, request.ProviderId, request.AppointmentId);

                return new SuccessDataResult<PatientInsuranceDataDto>(patient);
            }
        }
    }
}
