using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using Entities.Concrete.PatientEntity;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientsBySearchParamsQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientSearchParamDto>>>
    {
        public string Search { get; set; }

        public class GetPatientsBySearchParamsQueryHandler : IRequestHandler<GetPatientsBySearchParamsQuery, IDataResult<IEnumerable<PatientSearchParamDto>>>
        {
            private readonly IPatientRepository _patientRepository;

            public GetPatientsBySearchParamsQueryHandler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientSearchParamDto>>> Handle(GetPatientsBySearchParamsQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientRepository.GetPatientsSearchParams(request.Search);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PatientSearchParamDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
