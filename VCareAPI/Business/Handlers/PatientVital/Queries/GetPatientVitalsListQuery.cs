using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Abstract.IPatientVitalsRepository;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.PatientVitalsEntity;
using Entities.Dtos.PatientVitalsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientVitalsListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientVitalsDto>>>
    {
        public int? PatientId { get; set; }
        public int? ProviderId { get; set; }
        public class GetPatientVitalsListQueryHandler : IRequestHandler<GetPatientVitalsListQuery, IDataResult<IEnumerable<PatientVitalsDto>>>
        {
            private readonly IPatientVitalsRepository _patientVitalsRepository;
            private readonly IMapper _mapper;
            public GetPatientVitalsListQueryHandler(IPatientVitalsRepository patientVitalsRepository, IMapper mapper)
            {
                _patientVitalsRepository = patientVitalsRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientVitalsDto>>> Handle(GetPatientVitalsListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _patientVitalsRepository.GetPatientVitalsSearchParams(request.PatientId, request.ProviderId);

                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                var convertedData = rawData.Select(x => _mapper.Map<PatientVitalsDto>(x)).ToList();

                return new PagedDataResult<IEnumerable<PatientVitalsDto>>(convertedData, rawData.Count(), request.PageNumber);

            }
        }
    }
}
