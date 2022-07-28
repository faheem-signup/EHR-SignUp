using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDiagnosisCodeRepository;
using Entities.Dtos.PatientDiagnosisCodeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDiagnosisCodes.Queries
{
    public class GetPatientDiagnosisCodeListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientDiagnosisCodeDto>>>
    {
        public int? PatientId { get; set; }
        public int? DiagnosisId { get; set; }
        public int? ProviderId { get; set; }
        public class GetPatientDiagnosisCodeListQueryHandler : IRequestHandler<GetPatientDiagnosisCodeListQuery, IDataResult<IEnumerable<PatientDiagnosisCodeDto>>>
        {
            private readonly IPatientDiagnosisCodeRepository _patientDiagnosisCodeRepository;
            private readonly IMapper _mapper;
            public GetPatientDiagnosisCodeListQueryHandler(IPatientDiagnosisCodeRepository patientDiagnosisCodeRepository, IMapper mapper)
            {
                _patientDiagnosisCodeRepository = patientDiagnosisCodeRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientDiagnosisCodeDto>>> Handle(GetPatientDiagnosisCodeListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _patientDiagnosisCodeRepository.GetPatientDiagnosisCodeSearchParams(request.PatientId, request.ProviderId, request.DiagnosisId);
                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                var convertedData = rawData.Select(x => _mapper.Map<PatientDiagnosisCodeDto>(x)).ToList();

                return new PagedDataResult<IEnumerable<PatientDiagnosisCodeDto>>(convertedData, rawData.Count(), request.PageNumber);

            }
        }
    }
}
