using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Diagnosises.Commands;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticeDocsRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Commands
{
    public class DeletePracticeDocsCommand : IRequest<IResult>
    {
        public int DocmentId { get; set; }

        public class DeletePracticeDocsCommandHandler : IRequestHandler<DeletePracticeDocsCommand, IResult>
        {
            private readonly IPracticeDocsRepository _practiceDocsRepository;

            public DeletePracticeDocsCommandHandler(IPracticeDocsRepository practiceDocsRepository)
            {
                _practiceDocsRepository = practiceDocsRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePracticeDocsCommand request, CancellationToken cancellationToken)
            {
                var practiceDocToDelete = await _practiceDocsRepository.GetAsync(x => x.DocmentId == request.DocmentId);

                practiceDocToDelete.isDeleted = true;
                _practiceDocsRepository.Update(practiceDocToDelete);
                await _practiceDocsRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
