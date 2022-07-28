using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceAuthorizationCPTRepository;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using DataAccess.Abstract.IPatientInsuranceRepository;
using Entities.Concrete.PatientInsuranceAuthorizationEntity;
using Entities.Dtos.PatientInsurancesDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsuranceAuthorizations.Queries
{
    public class GetPatientInsuranceAuthorizationListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientInsuranceAuthorizationDto>>>
    {
        public int PatientId { get; set; }

        public class GetPatientInsuranceAuthorizationListQueryHandler : IRequestHandler<GetPatientInsuranceAuthorizationListQuery, IDataResult<IEnumerable<PatientInsuranceAuthorizationDto>>>
        {
            private readonly IPatientInsuranceAuthorizationRepository _patientInsuranceAuthorizationRepository;
            private readonly IMapper _mapper;
            private readonly IPatientInsuranceAuthorizationCPTRepository _patientInsuranceAuthorizationCPTRepository;
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;

            public GetPatientInsuranceAuthorizationListQueryHandler(IPatientInsuranceAuthorizationRepository patientInsuranceAuthorizationRepository, IMapper mapper, IPatientInsuranceAuthorizationCPTRepository patientInsuranceAuthorizationCPTRepository, IPatientInsuranceRepository patientInsuranceRepository)
            {
                _patientInsuranceAuthorizationRepository = patientInsuranceAuthorizationRepository;
                _mapper = mapper;
                _patientInsuranceAuthorizationCPTRepository = patientInsuranceAuthorizationCPTRepository;
                _patientInsuranceRepository = patientInsuranceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientInsuranceAuthorizationDto>>> Handle(GetPatientInsuranceAuthorizationListQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<int> insuranceIdList = null;
                var patientinsuranceList = await _patientInsuranceRepository.GetListAsync(x => x.PatientId == request.PatientId && x.IsDeleted != true);
                if (patientinsuranceList != null && patientinsuranceList.Count() > 0)
                {
                    insuranceIdList = patientinsuranceList.Select(x => x.PatientInsuranceId);
                }

                var list = await _patientInsuranceAuthorizationRepository.GetPatientInsuranceAuthorization();

                List<PatientInsuranceAuthorization> finalList = new List<PatientInsuranceAuthorization>();

                insuranceIdList.ToList().ForEach(x =>
                {
                    list.ToList().ForEach(a =>
                    {
                        if (x == a.PatientInsuranceId)
                        {
                            finalList.Add(a);
                        }
                    });
                });

                var pagedData = Paginate(finalList, request.PageNumber, request.PageSize);
                var convertedData = pagedData.Select(x => _mapper.Map<PatientInsuranceAuthorizationDto>(x)).ToList();
                convertedData.ToList().ForEach(async x =>
                {
                    var cptList = await _patientInsuranceAuthorizationCPTRepository.GetPatientInsuranceAuthorizationCPTList(x.PatientInsuranceAuthorizationId);
                    if (cptList != null)
                    {
                        var code = string.Empty;
                        cptList.ToList().ForEach(b =>
                        {
                            if (b.procedures != null)
                            {
                                code += b.procedures.Code + ',';
                            }
                        });
                        x.ProcedureCode = code.TrimEnd(','); //string.Join(",", cptList.Select(a => (a.procedures != null ? a.procedures.Code :"")));
                    }
                });

                if (convertedData.Count() > 0)
                {
                    convertedData = convertedData.OrderByDescending(x => x.PatientInsuranceAuthorizationId).ToList();
                }

                return new PagedDataResult<IEnumerable<PatientInsuranceAuthorizationDto>>(convertedData, finalList.Count(), request.PageNumber);
            }
        }
    }
}
