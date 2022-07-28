using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IInsuranceRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Insurances.Commands
{
    public class DeleteInsurancesCommand : IRequest<IResult>
    {
        public int InsuranceId { get; set; }
        public class DeleteInsurancesCommandHandler : IRequestHandler<DeleteInsurancesCommand, IResult>
        {
            private readonly IInsuranceRepository _insuranceRepository;

            public DeleteInsurancesCommandHandler(IInsuranceRepository insuranceRepository)
            {
                _insuranceRepository = insuranceRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteInsurancesCommand request, CancellationToken cancellationToken)
            {
                var insuranceToDelete = await _insuranceRepository.GetAsync(x => x.InsuranceId == request.InsuranceId);
                insuranceToDelete.IsDeleted = true;
                _insuranceRepository.Update(insuranceToDelete);
                await _insuranceRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
