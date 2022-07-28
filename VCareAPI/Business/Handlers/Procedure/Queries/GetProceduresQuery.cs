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
    public class GetProceduresQuery : BasePaginationQuery<IDataResult<IEnumerable<ProcedureDto>>>
    {
        public int PracticeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CPTGroupId { get; set; }

        public class GetProceduresQueryHandler : IRequestHandler<GetProceduresQuery, IDataResult<IEnumerable<ProcedureDto>>>
        {
            private readonly IProceduresRepository _proceduresRepository;

            public GetProceduresQueryHandler(IProceduresRepository proceduresRepository)
            {
                _proceduresRepository = proceduresRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProcedureDto>>> Handle(GetProceduresQuery request, CancellationToken cancellationToken)
            {
                var list = await _proceduresRepository.GetAllProcedure(request.PracticeId, request.Code, request.Description, request.CPTGroupId);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ProcedureDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
