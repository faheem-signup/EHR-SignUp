using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DocumentParentCategoryLookups.Queries
{
    public class GetDocumentParentCategoryLookupQuery : IRequest<IDataResult<DocumentParentCategoryLookup>>
    {
        public int ParentCategoryId { get; set; }

        public class GetDocumentParentCategoryLookupQueryHandler : IRequestHandler<GetDocumentParentCategoryLookupQuery, IDataResult<DocumentParentCategoryLookup>>
        {
            private readonly IDocumentParentCategoryLookupRepository _documentParentCategoryLookupRepository;

            public GetDocumentParentCategoryLookupQueryHandler(IDocumentParentCategoryLookupRepository documentParentCategoryLookupRepository)
            {
                _documentParentCategoryLookupRepository = documentParentCategoryLookupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<DocumentParentCategoryLookup>> Handle(GetDocumentParentCategoryLookupQuery request, CancellationToken cancellationToken)
            {
                var DocumentParentCategoryLookup = await _documentParentCategoryLookupRepository.GetAsync(p => p.ParentCategoryId == request.ParentCategoryId);
                return new SuccessDataResult<DocumentParentCategoryLookup>(DocumentParentCategoryLookup);
            }
        }
    }
}
