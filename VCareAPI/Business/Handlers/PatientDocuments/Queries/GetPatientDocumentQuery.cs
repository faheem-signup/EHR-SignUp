using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPatientDocumentRepository;
using Entities.Concrete.PatientDocumentEntity;
using Entities.Dtos.PatientDocumentsDto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocuments.Queries
{
    public class GetPatientDocumentQuery : IRequest<IDataResult<PatientDocumentDto>>
    {
        public int PatientDocumentId { get; set; }

        public class GetPatientDocumentQueryHandler : IRequestHandler<GetPatientDocumentQuery, IDataResult<PatientDocumentDto>>
        {
            private readonly IPatientDocumentRepository _patientDocumentRepository;

            public GetPatientDocumentQueryHandler(IPatientDocumentRepository patientDocumentRepository)
            {
                _patientDocumentRepository = patientDocumentRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PatientDocumentDto>> Handle(GetPatientDocumentQuery request, CancellationToken cancellationToken)
            {
                var patientDocument = await _patientDocumentRepository.GetPatientDocumentById(request.PatientDocumentId);
                return new SuccessDataResult<PatientDocumentDto>(patientDocument);
            }
        }
    }
}
