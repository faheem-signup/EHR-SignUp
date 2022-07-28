using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Diagnosises.Queries
{
    public class GetDiagnosisByCodeQuery : IRequest<IDataResult<IEnumerable<DiagnosisCode>>>
    {
        public string DignosisCode { get; set; }

        public class GetDiagnosisByCodeQueryHandler : IRequestHandler<GetDiagnosisByCodeQuery, IDataResult<IEnumerable<DiagnosisCode>>>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;

            public GetDiagnosisByCodeQueryHandler(IDiagnosisRepository diagnosiseRepository)
            {
                _diagnosiseRepository = diagnosiseRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<DiagnosisCode>>> Handle(GetDiagnosisByCodeQuery request, CancellationToken cancellationToken)
            {
                var list = await _diagnosiseRepository.GetListAsync(x => x.IsDeleted != true && x.Code.Contains(request.DignosisCode));
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.DiagnosisId).ToList();
                }

                return new SuccessDataResult<IEnumerable<DiagnosisCode>>(list);

            }
        }
    }
}
