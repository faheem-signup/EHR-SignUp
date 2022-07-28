using AutoMapper;
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Dtos.PatientInsurancesDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsuranceAuthorizations.Queries
{
    public class GetPatientInsuranceAuthorizationQuery : IRequest<IDataResult<PatientInsuranceAuthorizationDto>>
    {
        public int PatientInsuranceAuthorizationId { get; set; }
        public class GetPatientInsuranceAuthorizationQueryHandler : IRequestHandler<GetPatientInsuranceAuthorizationQuery, IDataResult<PatientInsuranceAuthorizationDto>>
        {
            private readonly IPatientInsuranceAuthorizationRepository _patientInsuranceAuthorizationRepository;
            private readonly IMapper _mapper;
            private readonly IPatientInsuranceAuthorizationCPTRepository _patientInsuranceAuthorizationCPTRepository;

            public GetPatientInsuranceAuthorizationQueryHandler(IPatientInsuranceAuthorizationRepository patientInsuranceAuthorizationRepository, IMapper mapper, IPatientInsuranceAuthorizationCPTRepository patientInsuranceAuthorizationCPTRepository)
            {
                _patientInsuranceAuthorizationRepository = patientInsuranceAuthorizationRepository;
                _mapper = mapper;
                _patientInsuranceAuthorizationCPTRepository = patientInsuranceAuthorizationCPTRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PatientInsuranceAuthorizationDto>> Handle(GetPatientInsuranceAuthorizationQuery request, CancellationToken cancellationToken)
            {
                var patientInsuranceAuthorization = await _patientInsuranceAuthorizationRepository.GetAsync(x => x.PatientInsuranceAuthorizationId == request.PatientInsuranceAuthorizationId && x.IsDeleted != true);

                var patientInsuranceAuthorizationObj = _mapper.Map<PatientInsuranceAuthorizationDto>(patientInsuranceAuthorization);

                var procedureDiagnosList = await _patientInsuranceAuthorizationCPTRepository.GetListAsync(x => x.PatientInsuranceAuthorizationId == request.PatientInsuranceAuthorizationId);
                if (procedureDiagnosList != null && procedureDiagnosList.Count() > 0)
                {
                    patientInsuranceAuthorizationObj.patientInsuranceAuthorizationCPTList = _mapper.Map<IEnumerable<PatientInsuranceAuthorizationCPTDto>>(procedureDiagnosList);
                }


                return new SuccessDataResult<PatientInsuranceAuthorizationDto>(patientInsuranceAuthorizationObj);
            }
        }
    }
}
