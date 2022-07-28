using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientEducationDocumentRepository;
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
    public class GetPatientEducationDocumentListQuery : IRequest<IDataResult<IEnumerable<PatientEducationDocument>>>
    {
        public int PatientId { get; set; }

        public class GetPatientEducationDocumentListQueryHandler : IRequestHandler<GetPatientEducationDocumentListQuery, IDataResult<IEnumerable<PatientEducationDocument>>>
        {
            private readonly IPatientEducationDocumentRepository _patientEducationDocumentRepository;

            public GetPatientEducationDocumentListQueryHandler(IPatientEducationDocumentRepository patientEducationDocumentRepository)
            {
                _patientEducationDocumentRepository = patientEducationDocumentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientEducationDocument>>> Handle(GetPatientEducationDocumentListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientEducationDocumentRepository.GetListAsync(x => x.PatientId == request.PatientId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.PatientEducationDocumentId).ToList();
                }

                return new SuccessDataResult<IEnumerable<PatientEducationDocument>>(list);
            }
        }
    }
}
