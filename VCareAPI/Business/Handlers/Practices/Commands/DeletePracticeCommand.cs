using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticesRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Commands
{
    public class DeletePracticeCommand : IRequest<IResult>
    {
        public int PracticeId { get; set; }

        public class DeletePracticeCommandHandler : IRequestHandler<DeletePracticeCommand, IResult>
        {
            private readonly IPracticesRepository _practiceRepository;

            public DeletePracticeCommandHandler(IPracticesRepository practiceRepository)
            {
                _practiceRepository = practiceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePracticeCommand request, CancellationToken cancellationToken)
            {
                var practiceToDelete = await _practiceRepository.GetAsync(x => x.PracticeId == request.PracticeId && x.IsDeleted != true);
                practiceToDelete.IsDeleted = true;
                _practiceRepository.Update(practiceToDelete);
                await _practiceRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
