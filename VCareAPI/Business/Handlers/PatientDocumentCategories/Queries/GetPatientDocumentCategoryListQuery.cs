using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientDocumentCategoryRepository;
using Entities.Concrete.PatientDocumentCategoryEntity;
using Entities.Dtos.PatientDocumentsCategoryDto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PatientDocumentCategories.Queries
{
    public class GetPatientDocumentCategoryListQuery : IRequest<IDataResult<IEnumerable<PatientDocumentCategoryDto>>>
    {
        public class GetPatientDocumentCategoryListQueryHandler : IRequestHandler<GetPatientDocumentCategoryListQuery, IDataResult<IEnumerable<PatientDocumentCategoryDto>>>
        {
            private readonly IPatientDocumentCategoryRepository _patientDocumentCategoryRepository;

            public GetPatientDocumentCategoryListQueryHandler(IPatientDocumentCategoryRepository patientDocumentCategoryRepository)
            {
                _patientDocumentCategoryRepository = patientDocumentCategoryRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PatientDocumentCategoryDto>>> Handle(GetPatientDocumentCategoryListQuery request, CancellationToken cancellationToken)
            {
                var list = await _patientDocumentCategoryRepository.GetPatientDocumentCategory();
                return new SuccessDataResult<IEnumerable<PatientDocumentCategoryDto>>(list.ToList());
            }
        }
    }
}
