using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientInsuranceAuthorizationRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientInsuranceAuthorizations.Commands
{
    public class DeletePatientInsuranceAuthorizationCommand : IRequest<IResult>
    {
        public int PatientInsuranceAuthorizationId { get; set; }
        public class DeletePatientInsuranceAuthorizationCommandHandler : IRequestHandler<DeletePatientInsuranceAuthorizationCommand, IResult>
        {
            private readonly IPatientInsuranceAuthorizationRepository _patientInsuranceAuthorizationRepository;

            public DeletePatientInsuranceAuthorizationCommandHandler(IPatientInsuranceAuthorizationRepository patientInsuranceAuthorizationRepository)
            {
                _patientInsuranceAuthorizationRepository = patientInsuranceAuthorizationRepository;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeletePatientInsuranceAuthorizationCommand request, CancellationToken cancellationToken)
            {
                var patientInsurance = await _patientInsuranceAuthorizationRepository.GetAsync(x => x.PatientInsuranceAuthorizationId == request.PatientInsuranceAuthorizationId);

                patientInsurance.IsDeleted = true;
                _patientInsuranceAuthorizationRepository.Update(patientInsurance);
                await _patientInsuranceAuthorizationRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
