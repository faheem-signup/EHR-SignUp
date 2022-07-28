using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Concrete.FormTemplatesEntity;
using Entities.Dtos.FormTemplateDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetFormTemplatesByProviderIdQuery : BasePaginationQuery<IDataResult<IEnumerable<FormTemplateDto>>>
    {
        public int? ProviderId { get; set; }
        public class GetFormTemplatesByProviderIdQueryHandler : IRequestHandler<GetFormTemplatesByProviderIdQuery, IDataResult<IEnumerable<FormTemplateDto>>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            public GetFormTemplatesByProviderIdQueryHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<FormTemplateDto>>> Handle(GetFormTemplatesByProviderIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _formTemplateRepository.GetFormTemplateList();
                if (list.Count() > 0)
                {
                    if (request.ProviderId != 0 && request.ProviderId != null)
                    {
                        list = list.Where(x => x.ProviderId == request.ProviderId).ToList();
                    }
                }
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<FormTemplateDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
