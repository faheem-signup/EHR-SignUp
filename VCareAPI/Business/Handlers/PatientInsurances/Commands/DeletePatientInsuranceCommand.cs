using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceRepository;
using DataAccess.Abstract.IReferralProviderRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsurances.Commands
{
    public class DeletePatientInsuranceCommand : IRequest<IResult>
    {
        public int PatientInsuranceId { get; set; }
        public class DeletePatientInsuranceCommandHandler : IRequestHandler<DeletePatientInsuranceCommand, IResult>
        {
            private readonly IPatientInsuranceRepository _patientInsuranceRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public DeletePatientInsuranceCommandHandler(IPatientInsuranceRepository patientInsuranceRepository, IHttpContextAccessor contextAccessor)
            {
                _patientInsuranceRepository = patientInsuranceRepository;
                _contextAccessor = contextAccessor;
            }

           // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientInsuranceCommand request, CancellationToken cancellationToken)
            {
                var exsistingPatientInsurance = await _patientInsuranceRepository.GetAsync(x => x.PatientInsuranceId == request.PatientInsuranceId);

                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (exsistingPatientInsurance != null)
                {
                    exsistingPatientInsurance.IsDeleted = true;
                    exsistingPatientInsurance.ModifiedBy = int.Parse(userId);
                    exsistingPatientInsurance.ModifiedDate = DateTime.Now;
                }
                _patientInsuranceRepository.Update(exsistingPatientInsurance);
                await _patientInsuranceRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
