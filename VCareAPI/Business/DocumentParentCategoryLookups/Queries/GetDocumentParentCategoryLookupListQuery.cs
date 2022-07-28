using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DocumentParentCategoryLookups.Queries
{
    public class GetDocumentParentCategoryLookupListQuery : BasePaginationQuery<IDataResult<IEnumerable<DocumentParentCategoryLookup>>>
    {
        public class GetDocumentParentCategoryLookupListQueryHandler : IRequestHandler<GetDocumentParentCategoryLookupListQuery, IDataResult<IEnumerable<DocumentParentCategoryLookup>>>
        {
            private readonly IDocumentParentCategoryLookupRepository _documentParentCategoryLookupRepository;

            public GetDocumentParentCategoryLookupListQueryHandler(IDocumentParentCategoryLookupRepository documentParentCategoryLookupRepository)
            {
                _documentParentCategoryLookupRepository = documentParentCategoryLookupRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<DocumentParentCategoryLookup>>> Handle(GetDocumentParentCategoryLookupListQuery request, CancellationToken cancellationToken)
            {
                var list = await _documentParentCategoryLookupRepository.GetListAsync();
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ParentCategoryId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<DocumentParentCategoryLookup>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
