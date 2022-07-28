using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Dtos.TemplateCategoryDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetAllCategoriesTemplatesQuery : IRequest<IDataResult<IEnumerable<TemplateCategoryDto>>>
    {
        public class GetAllCategoriesTemplatesQueryHandler : IRequestHandler<GetAllCategoriesTemplatesQuery, IDataResult<IEnumerable<TemplateCategoryDto>>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            public GetAllCategoriesTemplatesQueryHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TemplateCategoryDto>>> Handle(GetAllCategoriesTemplatesQuery request, CancellationToken cancellationToken)
            {
                var list = await _formTemplateRepository.GetAllCategoriesTemplates();
                return new SuccessDataResult<IEnumerable<TemplateCategoryDto>>(list);
            }
        }
    }
}
