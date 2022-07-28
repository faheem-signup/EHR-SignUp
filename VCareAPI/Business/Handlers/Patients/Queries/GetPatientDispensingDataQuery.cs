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
    public class GetPatientDispensingDataQuery : IRequest<IDataResult<PatientDespensingDataDto>>
    {
        public int PatientId { get; set; }

        public class GetPatientDispensingDataQueryHandler : IRequestHandler<GetPatientDispensingDataQuery, IDataResult<PatientDespensingDataDto>>
        {
            private readonly IPatientRepository _patientRepository;

            public GetPatientDispensingDataQueryHandler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<PatientDespensingDataDto>> Handle(GetPatientDispensingDataQuery request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetPatientDispensingDataById(request.PatientId);

                return new SuccessDataResult<PatientDespensingDataDto>(patient);
            }
        }
    }
}
