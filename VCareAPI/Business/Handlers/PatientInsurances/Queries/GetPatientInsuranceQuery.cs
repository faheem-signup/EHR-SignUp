using AutoMapper;
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Dtos.PatientInsurancesDto;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Queries
{
    public class GetPatientInsuranceQuery : IRequest<IDataResult<PatientInsurancesDto>>
    {
        public int PatientInsuranceId { get; set; }

        public class GetPatientInsuranceQueryHandler : IRequestHandler<GetPatientInsuranceQuery, IDataResult<PatientInsurancesDto>>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IMapper _mapper;

            public GetPatientInsuranceQueryHandler(IPatientInsuranceRepository patientInsuranceRepository)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PatientInsurancesDto>> Handle(GetPatientInsuranceQuery request, CancellationToken cancellationToken)
            {
                var patientInsuranceObj = await _patientInsuranceRepository.GetPatientInsuranceById(request.PatientInsuranceId);
               
                return new SuccessDataResult<PatientInsurancesDto>(patientInsuranceObj);
            }
        }
    }
}
