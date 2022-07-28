using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPatientProvideReferralRepository;
using Entities.Concrete.PatientProviderEntity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Patients.Queries
{
    public class GetPatientProvideReferringList : IRequest<IDataResult<IEnumerable<PatientProvideReferring>>>
    {
        public int PatientId { get; set; }

        public class GetPatientProvideReferringListHandler : IRequestHandler<GetPatientProvideReferringList, IDataResult<IEnumerable<PatientProvideReferring>>>
        {
            private readonly IPatientProvideReferralRepository _patientProvideReferralRepository;

            public GetPatientProvideReferringListHandler(IPatientProvideReferralRepository patientProvideReferralRepository)
            {
                _patientProvideReferralRepository = patientProvideReferralRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientProvideReferring>>> Handle(GetPatientProvideReferringList request, CancellationToken cancellationToken)
            {
                var list = await _patientProvideReferralRepository.GetListAsync(x => x.PatientId == request.PatientId);

                return new SuccessDataResult<IEnumerable<PatientProvideReferring>>(list.ToList());
            }
        }
    }
}
