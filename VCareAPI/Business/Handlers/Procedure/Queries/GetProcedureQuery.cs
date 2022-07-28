using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IProceduresRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Procedure.Queries
{
    public class GetProcedureQuery : IRequest<IDataResult<Entities.Concrete.ProceduresEntity.Procedure>>
    {
        public int ProcedureId { get; set; }

        public class GetProcedureQueryHandler : IRequestHandler<GetProcedureQuery, IDataResult<Entities.Concrete.ProceduresEntity.Procedure>>
        {
            private readonly IProceduresRepository _proceduresRepository;

            public GetProcedureQueryHandler(IProceduresRepository proceduresRepository)
            {
                _proceduresRepository = proceduresRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Entities.Concrete.ProceduresEntity.Procedure>> Handle(GetProcedureQuery request, CancellationToken cancellationToken)
            {
                var procedure = await _proceduresRepository.GetAsync(x => x.ProcedureId == request.ProcedureId);

                return new SuccessDataResult<Entities.Concrete.ProceduresEntity.Procedure>(procedure);
            }
        }
    }
}
