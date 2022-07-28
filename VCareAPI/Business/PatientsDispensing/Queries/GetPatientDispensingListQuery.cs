using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingRepository;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensing.Queries
{
    public class GetPatientDispensingListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientDispensingDto>>>
    {
        public int? PatientId { get; set; }

        public class GetPatientDispensingListQueryHandler : IRequestHandler<GetPatientDispensingListQuery, IDataResult<IEnumerable<PatientDispensingDto>>>
        {
            private readonly IPatientDispensingRepository _patientDispensingRepository;
            private readonly IMapper _mapper;

            public GetPatientDispensingListQueryHandler(IPatientDispensingRepository patientDispensingRepository, IMapper mapper)
            {
                _patientDispensingRepository = patientDispensingRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientDispensingDto>>> Handle(GetPatientDispensingListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _patientDispensingRepository.GetPatientDispencingList((int)request.PatientId);
                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PatientDispensingDto>>(dataList, rawData.Count(), request.PageNumber);
            }
        }
    }
}
