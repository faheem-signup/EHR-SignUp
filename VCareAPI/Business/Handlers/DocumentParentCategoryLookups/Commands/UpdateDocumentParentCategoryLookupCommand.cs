using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using Entities.Concrete;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DocumentParentCategoryLookups.Commands
{
    public class UpdateDocumentParentCategoryLookupCommand : IRequest<IResult>
    {
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
        public class UpdateDocumentParentCategoryLookupCommandHandler : IRequestHandler<UpdateDocumentParentCategoryLookupCommand, IResult>
        {
            private readonly IDocumentParentCategoryLookupRepository _documentParentCategoryLookupRepository;

            public UpdateDocumentParentCategoryLookupCommandHandler(IDocumentParentCategoryLookupRepository documentParentCategoryLookupRepository)
            {
                _documentParentCategoryLookupRepository = documentParentCategoryLookupRepository;
            }

            //   [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateDocumentParentCategoryLookupCommand request, CancellationToken cancellationToken)
            {
                var existingData = await _documentParentCategoryLookupRepository.GetAsync(x => x.ParentCategoryId == request.ParentCategoryId);
                if (existingData != null)
                {
                    existingData.Name = request.Name;
                    _documentParentCategoryLookupRepository.Update(existingData);
                    await _documentParentCategoryLookupRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
