using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using Entities.Concrete;
using Entities.Dtos.DiagnosisCodeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Diagnosises.Queries
{
    public class GetAllDiagnosisListQuery : BasePaginationQuery<IDataResult<IEnumerable<DiagnosisCode>>>
    {
        public class GetAllDiagnosisListQueryHandler : IRequestHandler<GetAllDiagnosisListQuery, IDataResult<IEnumerable<DiagnosisCode>>>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;
            private readonly IMapper _mapper;

            public GetAllDiagnosisListQueryHandler(IDiagnosisRepository diagnosiseRepository, IMapper mapper)
            {
                _diagnosiseRepository = diagnosiseRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<DiagnosisCode>>> Handle(GetAllDiagnosisListQuery request, CancellationToken cancellationToken)
            {
                var rawData = await _diagnosiseRepository.GetListAsync();
                if (rawData.Count() > 0)
                {
                    rawData = rawData.OrderByDescending(x => x.DiagnosisId).ToList();
                }

                var dataList = Paginate(rawData, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<DiagnosisCode>>(dataList, rawData.Count(), request.PageNumber);
            }
        }
    }
}
