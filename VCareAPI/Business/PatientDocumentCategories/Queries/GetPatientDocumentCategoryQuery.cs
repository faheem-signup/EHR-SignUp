using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using Entities.Concrete.PatientDocumentCategoryEntity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocumentCategories.Queries
{
    public class GetPatientDocumentCategoryQuery : IRequest<IDataResult<PatientDocumentCategory>>
    {
        public int PatientDocCateogryId { get; set; }

        public class GetPatientDocumentCategoryQueryHandler : IRequestHandler<GetPatientDocumentCategoryQuery, IDataResult<PatientDocumentCategory>>
        {
            private readonly IPatientDocumentCategoryRepository _patientDocumentCategoryRepository;

            public GetPatientDocumentCategoryQueryHandler(IPatientDocumentCategoryRepository patientDocumentCategoryRepository)
            {
                _patientDocumentCategoryRepository = patientDocumentCategoryRepository;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PatientDocumentCategory>> Handle(GetPatientDocumentCategoryQuery request, CancellationToken cancellationToken)
            {
                var patientDocumentCategory = await _patientDocumentCategoryRepository.GetAsync(p => p.PatientDocCateogryId == request.PatientDocCateogryId);
                return new SuccessDataResult<PatientDocumentCategory>(patientDocumentCategory);
            }
        }
    }
}
