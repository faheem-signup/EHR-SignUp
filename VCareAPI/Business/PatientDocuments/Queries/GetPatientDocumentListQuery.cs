using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentRepository;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Dtos.PatientDocumentsDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocuments.Queries
{
    public class GetPatientDocumentListQuery : BasePaginationQuery<IDataResult<IEnumerable<PatientDocumentDto>>>
    {
        public int PatientID { get; set; }
        public int PatientDocCateogryId { get; set; }
        public class GetPatientDocumentListQueryHandler : IRequestHandler<GetPatientDocumentListQuery, IDataResult<IEnumerable<PatientDocumentDto>>>
        {
            private readonly IPatientDocumentRepository _patientDocumentRepository;
            public GetPatientDocumentListQueryHandler(IPatientDocumentRepository patientDocumentRepository)
            {
                _patientDocumentRepository = patientDocumentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientDocumentDto>>> Handle(GetPatientDocumentListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientDocumentRepository.GetPatientDocumentCategory(request.PatientID, request.PatientDocCateogryId);
                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PatientDocumentDto>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
