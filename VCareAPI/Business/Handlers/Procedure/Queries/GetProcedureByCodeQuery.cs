using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDiagnosisRepository;
using DataAccess.Abstract.IProceduresRepository;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Queries
{
    public class GetProcedureByCodeQuery : IRequest<IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>>
    {
        public string Code { get; set; }
        public class GetProcedureByCodeQueryHandler : IRequestHandler<GetProcedureByCodeQuery, IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>>
        {
            private readonly IProceduresRepository _proceduresRepository;
            public GetProcedureByCodeQueryHandler(IProceduresRepository proceduresRepository)
            {
                _proceduresRepository = proceduresRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>> Handle(GetProcedureByCodeQuery request, CancellationToken cancellationToken)
            {
                var list = await _proceduresRepository.GetListAsync(x => x.Code.Contains(request.Code));
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ProcedureId).ToList();
                }

                return new SuccessDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>(list);

            }
        }
    }
}
