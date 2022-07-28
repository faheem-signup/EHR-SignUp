using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using Entities.Concrete.PatientInsuranceEntity;
using Entities.Concrete.ReferralProviderEntity;
using Entities.Dtos.PatientInsurancesDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Queries
{
    public class GetDeletedPatientInsuranceListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientInsurancesDto>>>
    {
        public int PatientId { get; set; }

        public class GetDeletedPatientInsuranceListQueryHandler : IRequestHandler<GetDeletedPatientInsuranceListQuery, IDataResult<IEnumerable<PatientInsurancesDto>>>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IMapper _mapper;

            public GetDeletedPatientInsuranceListQueryHandler(IPatientInsuranceRepository patientInsuranceRepository, IMapper mapper)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientInsurancesDto>>> Handle(GetDeletedPatientInsuranceListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientInsuranceRepository.GetDeletedPatientInsurance(request.PatientId);
                var dataList = Paginate(list, request.PageNumber, request.PageSize);
                var convertedData = list.Select(x => _mapper.Map<PatientInsurancesDto>(x)).ToList();
                if (convertedData.Count() > 0)
                {
                    convertedData = convertedData.OrderByDescending(x => x.PatientInsuranceId).ToList();
                }

                return new PagedDataResult<IEnumerable<PatientInsurancesDto>>(convertedData, list.Count(), request.PageNumber);
            }
        }
    }
}
