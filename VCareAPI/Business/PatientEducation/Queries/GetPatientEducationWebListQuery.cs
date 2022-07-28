using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEducationWebRepository;
using Entities.Concrete.PatientEducationEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientEducation.Queries
{
    public class GetPatientEducationWebListQuery : IRequest<IDataResult<IEnumerable<PatientEducationWeb>>>
    {
        public int PatientId { get; set; }

        public class GetPatientEducationWebListQueryHandler : IRequestHandler<GetPatientEducationWebListQuery, IDataResult<IEnumerable<PatientEducationWeb>>>
        {
            private readonly IPatientEducationWebRepository _patientEducationWebRepository;

            public GetPatientEducationWebListQueryHandler(IPatientEducationWebRepository patientEducationWebRepository)
            {
                _patientEducationWebRepository = patientEducationWebRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientEducationWeb>>> Handle(GetPatientEducationWebListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientEducationWebRepository.GetListAsync(x => x.PatientId == request.PatientId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.PatientEducationWebId).ToList();
                }

                return new SuccessDataResult<IEnumerable<PatientEducationWeb>>(list);
            }
        }
    }
}
