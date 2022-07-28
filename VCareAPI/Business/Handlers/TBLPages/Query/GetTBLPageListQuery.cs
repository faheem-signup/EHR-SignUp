using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ILocationRepository;
using DataAccess.Abstract.ITBLPageRepository;
using Entities.Concrete.Location;
using Entities.Concrete.TBLPageEntity;
using Entities.Concrete.User;
using Entities.Dtos.TblPageDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TBLPages.Queries
{
    public class GetTBLPageListQuery : BasePaginationQuery<IDataResult<IEnumerable<TblSubPageDto>>>
    {
        public class GetTBLPageListQueryHandler : IRequestHandler<GetTBLPageListQuery, IDataResult<IEnumerable<TblSubPageDto>>>
        {
            private readonly ITBLPageRepository _tBLPageRepository;

            public GetTBLPageListQueryHandler(ITBLPageRepository tBLPageRepository)
            {
                _tBLPageRepository = tBLPageRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TblSubPageDto>>> Handle(GetTBLPageListQuery request, CancellationToken cancellationToken)
            {
                var list = await _tBLPageRepository.GetAllModulesList();
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<TblSubPageDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
