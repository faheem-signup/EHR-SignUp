using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Concrete.TemplateCategoryEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetTemplateCategoriesQuery : IRequest<IDataResult<IEnumerable<TemplateCategory>>>
    {
        public class GetTemplateCategoriesQueryHandler : IRequestHandler<GetTemplateCategoriesQuery, IDataResult<IEnumerable<TemplateCategory>>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            public GetTemplateCategoriesQueryHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TemplateCategory>>> Handle(GetTemplateCategoriesQuery request, CancellationToken cancellationToken)
            {
                var list = await _formTemplateRepository.GetTemplateCategory();
                return new SuccessDataResult<IEnumerable<TemplateCategory>>(list);
            }
        }
    }
}
