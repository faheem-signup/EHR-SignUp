using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Concrete.FormTemplatesEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetFormTemplatesQuery : BasePaginationQuery<IDataResult<IEnumerable<FormTemplate>>>
    {
        public string TemplateName { get; set; }
        public string Speciality { get; set; }
        public int TemplateCategoryId { get; set; }
        public class GetFormTemplatesQueryHandler : IRequestHandler<GetFormTemplatesQuery, IDataResult<IEnumerable<FormTemplate>>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            public GetFormTemplatesQueryHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<FormTemplate>>> Handle(GetFormTemplatesQuery request, CancellationToken cancellationToken)
            {
                var list = await _formTemplateRepository.GetFormTemplateSearchParams(request.TemplateName, request.TemplateCategoryId, request.Speciality);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<FormTemplate>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
