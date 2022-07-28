using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using Entities.Dtos.FormTemplateDto;
using Entities.Dtos.TemplateCategoryDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Queries
{
    public class GetNotesQuery : BasePaginationQuery<IDataResult<IEnumerable<NotesDto>>>
    {
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IDataResult<IEnumerable<NotesDto>>>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;
            public GetNotesQueryHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<NotesDto>>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
            {
                var _list = await _formTemplateRepository.GetNotes(request.PatientId, request.ProviderId);

                return new PagedDataResult<IEnumerable<NotesDto>>(_list, _list.Count(), request.PageNumber);
            }
        }
    }
}
