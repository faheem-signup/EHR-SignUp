using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IFormTemplateRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.FormTemplates.Commands
{
    public class DeleteFormTemplateCommand : IRequest<IResult>
    {
        public int TemplateId { get; set; }

        public class DeleteDiagnosisCommandHandler : IRequestHandler<DeleteFormTemplateCommand, IResult>
        {
            private readonly IFormTemplateRepository _formTemplateRepository;

            public DeleteDiagnosisCommandHandler(IFormTemplateRepository formTemplateRepository)
            {
                _formTemplateRepository = formTemplateRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteFormTemplateCommand request, CancellationToken cancellationToken)
            {
                var formTemplateToDelete = await _formTemplateRepository.GetAsync(x => x.TemplateId == request.TemplateId);
                //formTemplateToDelete.IsDeleted = true;
                //_formTemplateRepository.Update(formTemplateToDelete);
                _formTemplateRepository.Delete(formTemplateToDelete);
                await _formTemplateRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
