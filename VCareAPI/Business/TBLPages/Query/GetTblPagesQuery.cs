using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ITBLPageRepository;
using Entities.Concrete.TBLPageEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TBLPages.Query
{
    public class GetTblPagesQuery : IRequest<IDataResult<IEnumerable<TblPage>>>
    {
        public class GetTblPagesQueryHandler : IRequestHandler<GetTblPagesQuery, IDataResult<IEnumerable<TblPage>>>
        {
            private readonly ITBLPageRepository _tBLPageRepository;

            public GetTblPagesQueryHandler(ITBLPageRepository tBLPageRepository)
            {
                _tBLPageRepository = tBLPageRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TblPage>>> Handle(GetTblPagesQuery request, CancellationToken cancellationToken)
            {
                var list = await _tBLPageRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<TblPage>>(list);
            }
        }
    }
}
