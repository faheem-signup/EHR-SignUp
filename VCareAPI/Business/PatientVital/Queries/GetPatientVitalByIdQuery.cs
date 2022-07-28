using Core.Utilities.Results;
using DataAccess.Abstract.IPatientVitalsRepository;
using Entities.Concrete.PatientVitalsEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientVital.Queries
{
    public class GetPatientVitalByIdQuery : IRequest<IDataResult<PatientVitals>>
    {
        public int PatientVitalsId { get; set; }
        public class GetPatientVitalByIdQueryHandler : IRequestHandler<GetPatientVitalByIdQuery, IDataResult<PatientVitals>>
        {
            private readonly IPatientVitalsRepository _patientVitalsRepository;
            public GetPatientVitalByIdQueryHandler(IPatientVitalsRepository patientVitalsRepository)
            {
                _patientVitalsRepository = patientVitalsRepository;
            }

            public async Task<IDataResult<PatientVitals>> Handle(GetPatientVitalByIdQuery request, CancellationToken cancellationToken)
            {
                var patientVitalsObj = await _patientVitalsRepository.GetPatientVitalsById(request.PatientVitalsId);

                return new SuccessDataResult<PatientVitals>(patientVitalsObj);
            }
        }
    }
}
