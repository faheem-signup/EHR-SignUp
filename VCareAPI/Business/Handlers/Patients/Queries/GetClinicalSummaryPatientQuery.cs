using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using Entities.Concrete.PatientEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Patients.Queries
{
    public class GetClinicalSummaryPatientQuery : IRequest<IDataResult<PatientDto>>
    {
        public int PatientId { get; set; }

        public class GetClinicalSummaryPatientQueryHandler : IRequestHandler<GetClinicalSummaryPatientQuery, IDataResult<PatientDto>>
        {
            private readonly IPatientRepository _patientRepository;
            public GetClinicalSummaryPatientQueryHandler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<PatientDto>> Handle(GetClinicalSummaryPatientQuery request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetClinicalSummaryPatient(request.PatientId);

                return new SuccessDataResult<PatientDto>(patient);
            }
        }
    }
}
