using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IBillingClaimRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BillingClaims.Commands
{
    public class DeleteBillingClaimCommand : IRequest<IResult>
    {
        public int BillingClaimsId { get; set; }

        public class DeleteBillingClaimCommandHandler : IRequestHandler<DeleteBillingClaimCommand, IResult>
        {
            private readonly IBillingClaimRepository _billingClaimRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public DeleteBillingClaimCommandHandler(IBillingClaimRepository billingClaimRepository, IHttpContextAccessor contextAccessor)
            {
                _billingClaimRepository = billingClaimRepository;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteBillingClaimCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var billingClaimToDelete = await _billingClaimRepository.GetAsync(x => x.BillingClaimsId == request.BillingClaimsId);
                billingClaimToDelete.IsDeleted = true;
                billingClaimToDelete.ModifiedDate = DateTime.Now;
                _billingClaimRepository.Update(billingClaimToDelete);
                await _billingClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
