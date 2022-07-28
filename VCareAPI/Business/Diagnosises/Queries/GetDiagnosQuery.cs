using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Diagnosises.Queries
{
    public class GetDiagnosQuery : IRequest<IDataResult<DiagnosisCode>>
    {
        public int DiagnosisId { get; set; }

        public class GetDiagnosQueryHandler : IRequestHandler<GetDiagnosQuery, IDataResult<DiagnosisCode>>
        {
            private readonly IDiagnosisRepository _diagnosiseRepository;
            public GetDiagnosQueryHandler(IDiagnosisRepository diagnosiseRepository)
            {
                _diagnosiseRepository = diagnosiseRepository;
            }

            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DiagnosisCode>> Handle(GetDiagnosQuery request, CancellationToken cancellationToken)
            {
                var diagnos = await _diagnosiseRepository.GetAsync(x => x.DiagnosisId == request.DiagnosisId && x.IsDeleted != true);

                return new SuccessDataResult<DiagnosisCode>(diagnos);
            }
        }
    }
}
