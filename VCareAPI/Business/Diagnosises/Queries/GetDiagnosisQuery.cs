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
    public class GetDiagnosisQuery : BasePaginationQuery<IDataResult<IEnumerable<DiagnosisCodeDto>>>
    {
        public int PracticeId { get; set; }
        public int? ICDCategoryId { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public class GetDiagnosisQueryHandler : IRequestHandler<GetDiagnosisQuery, IDataResult<IEnumerable<DiagnosisCodeDto>>>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;
            private readonly IMapper _mapper;
            public GetDiagnosisQueryHandler(IDiagnosisRepository diagnosiseRepository, IMapper mapper)
            {
                _diagnosiseRepository = diagnosiseRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<DiagnosisCodeDto>>> Handle(GetDiagnosisQuery request, CancellationToken cancellationToken)
            {
                var list = await _diagnosiseRepository.GetDiagnosesList(request.PracticeId, request.ICDCategoryId, request.Code, request.ShortDescription, request.Description);

                return new PagedDataResult<IEnumerable<DiagnosisCodeDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
