using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DocumentParentCategoryLookups.Commands
{
    public class DeleteDocumentParentCategoryLookupCommand : IRequest<IResult>
    {
        public int ParentCategoryId { get; set; }
        public class DeleteDocumentParentCategoryLookupCommandHandler : IRequestHandler<DeleteDocumentParentCategoryLookupCommand, IResult>
        {
            private readonly IDocumentParentCategoryLookupRepository _documentParentCategoryLookupRepository;

            public DeleteDocumentParentCategoryLookupCommandHandler(IDocumentParentCategoryLookupRepository documentParentCategoryLookupRepository)
            {
                _documentParentCategoryLookupRepository = documentParentCategoryLookupRepository;
            }

          //  [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteDocumentParentCategoryLookupCommand request, CancellationToken cancellationToken)
            {
                var documentParentCategoryLookupToDelete = await _documentParentCategoryLookupRepository.GetAsync(x => x.ParentCategoryId == request.ParentCategoryId);

                _documentParentCategoryLookupRepository.Delete(documentParentCategoryLookupToDelete);
                await _documentParentCategoryLookupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
