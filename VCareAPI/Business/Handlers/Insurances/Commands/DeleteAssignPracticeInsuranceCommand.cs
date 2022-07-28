using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAssignInsuranceRepository;
using DataAccess.Abstract.IInsuranceRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Commands
{
    public class DeleteAssignPracticeInsuranceCommand : IRequest<IResult>
    {
        public int PracticeInsuranceId { get; set; }
        public class DeleteAssignPracticeInsuranceCommandHandler : IRequestHandler<DeleteAssignPracticeInsuranceCommand, IResult>
        {
            private readonly IAssignInsuranceRepository _assignInsuranceRepository;

            public DeleteAssignPracticeInsuranceCommandHandler(IInsuranceRepository insuranceRepository, IAssignInsuranceRepository assignInsuranceRepository)
            {
                _assignInsuranceRepository = assignInsuranceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteAssignPracticeInsuranceCommand request, CancellationToken cancellationToken)
            {
                var assignInsuranceToDelete = await _assignInsuranceRepository.GetAsync(x => x.PracticeInsuranceId == request.PracticeInsuranceId);
                if(assignInsuranceToDelete != null)
                {
                    _assignInsuranceRepository.Delete(assignInsuranceToDelete);
                    await _assignInsuranceRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
