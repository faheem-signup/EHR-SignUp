using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDispensingDosingRepository;
using Entities.Dtos.PatientDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientsDispensingDosing.Queries
{
    public class GetPatientDispensingDosingListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientDispensingDosingDto>>>
    {
        public int? PatientId { get; set; }
        public class GetPatientDispensingDosingListQueryHandler : IRequestHandler<GetPatientDispensingDosingListQuery, IDataResult<IEnumerable<PatientDispensingDosingDto>>>
        {
            private readonly IPatientDispensingDosingRepository _patientDispensingDosingRepository;
            private readonly IMapper _mapper;
            public GetPatientDispensingDosingListQueryHandler(IPatientDispensingDosingRepository patientDispensingDosingRepository, IMapper mapper)
            {
                _patientDispensingDosingRepository = patientDispensingDosingRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientDispensingDosingDto>>> Handle(GetPatientDispensingDosingListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _patientDispensingDosingRepository.GetPatientDispencingDosingList((int)request.PatientId);

                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);

                return new PagedDataResult<IEnumerable<PatientDispensingDosingDto>>(dataList, rawData.Count(), request.PageNumber);
            }
        }
    }
}
