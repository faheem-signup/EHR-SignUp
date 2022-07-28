using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IDocumentParentCategoryLookupRepository;
using Entities.Concrete.DocumentParentCategoryLookupeEntity;
using Entities.Concrete.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DocumentParentCategoryLookups.Commands
{

    public class CreateDocumentParentCategoryLookupCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public class CreateDocumentParentCategoryLookupCommandHandler : IRequestHandler<CreateDocumentParentCategoryLookupCommand, IResult>
        {
            private readonly IDocumentParentCategoryLookupRepository _documentParentCategoryLookupRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateDocumentParentCategoryLookupCommandHandler(IDocumentParentCategoryLookupRepository documentParentCategoryLookupRepository, IMediator mediator, IMapper mapper)
            {
                _documentParentCategoryLookupRepository = documentParentCategoryLookupRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
           // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDocumentParentCategoryLookupCommand request, CancellationToken cancellationToken)
            {

                DocumentParentCategoryLookup documentPrentLookupObj = new DocumentParentCategoryLookup
                {
                    Name = request.Name,
                };

                _documentParentCategoryLookupRepository.Add(documentPrentLookupObj);
                await _documentParentCategoryLookupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

