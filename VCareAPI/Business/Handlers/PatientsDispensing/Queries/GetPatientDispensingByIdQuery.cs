using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingRepository;
using Entities.Concrete.PatientDispensingEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensing.Queries
{
    public class GetPatientDispensingByIdQuery : IRequest<IDataResult<PatientDispensing>>
    {
        public int DispensingId { get; set; }
        public class GetPatientDispensingByIdQueryHandler : IRequestHandler<GetPatientDispensingByIdQuery, IDataResult<PatientDispensing>>
        {
            private readonly IPatientDispensingRepository _patientDispensingRepository;
            public GetPatientDispensingByIdQueryHandler(IPatientDispensingRepository patientDispensingRepository)
            {
                _patientDispensingRepository = patientDispensingRepository;
            }

            public async Task<IDataResult<PatientDispensing>> Handle(GetPatientDispensingByIdQuery request, CancellationToken cancellationToken)
            {
                var PatientDispensingsObj = await _patientDispensingRepository.GetAsync(x => x.DispensingId == request.DispensingId);

                return new SuccessDataResult<PatientDispensing>(PatientDispensingsObj);
            }
        }
    }
}
