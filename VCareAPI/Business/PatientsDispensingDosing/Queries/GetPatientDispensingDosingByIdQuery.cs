using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingDosingRepository;
using Entities.Concrete.PatientDispensingDosingEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensingDosing.Queries
{
    public class GetPatientDispensingDosingByIdQuery : IRequest<IDataResult<PatientDispensingDosing>>
    {
        public int DispensingDosingId { get; set; }
        public class GetPatientDispensingDosingByIdQueryHandler : IRequestHandler<GetPatientDispensingDosingByIdQuery, IDataResult<PatientDispensingDosing>>
        {
            private readonly IPatientDispensingDosingRepository _patientDispensingDosingRepository;
            public GetPatientDispensingDosingByIdQueryHandler(IPatientDispensingDosingRepository patientDispensingDosingRepository)
            {
                _patientDispensingDosingRepository = patientDispensingDosingRepository;
            }

            public async Task<IDataResult<PatientDispensingDosing>> Handle(GetPatientDispensingDosingByIdQuery request, CancellationToken cancellationToken)
            {
                var patientDispensingDosingsObj = await _patientDispensingDosingRepository.GetAsync(x => x.DispensingDosingId == request.DispensingDosingId);

                return new SuccessDataResult<PatientDispensingDosing>(patientDispensingDosingsObj);
            }
        }
    }
}
