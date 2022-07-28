using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IProceduresRepository;
using Entities.Dtos.ProcedureDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Queries
{
    public class GetAllProceduresListQuery : BasePaginationQuery<IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>>
    {
        public class GetAllProceduresListQueryHandler : IRequestHandler<GetAllProceduresListQuery, IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>>
        {
            private readonly IProceduresRepository _proceduresRepository;
            public GetAllProceduresListQueryHandler(IProceduresRepository proceduresRepository)
            {
                _proceduresRepository = proceduresRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>> Handle(GetAllProceduresListQuery request, CancellationToken cancellationToken)
            {
                var list = await _proceduresRepository.GetListAsync();
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ProcedureId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Entities.Concrete.ProceduresEntity.Procedure>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
