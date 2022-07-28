using Azure.Storage.Blobs;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticeDocsRepository;
using Entities.Concrete.PracticeDocsEntity;
using Entities.Dtos.AzureFileDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetPracticeDocsQuery : BasePaginationQuery<IDataResult<IEnumerable<PracticeDoc>>>
    {
        public int PracticeId { get; set; }
        public class GetPracticeDocsQueryHandler : IRequestHandler<GetPracticeDocsQuery, IDataResult<IEnumerable<PracticeDoc>>>
        {
            private readonly IPracticeDocsRepository _practiceDocsRepository;

            public GetPracticeDocsQueryHandler(IPracticeDocsRepository practiceDocsRepository)
            {
                _practiceDocsRepository = practiceDocsRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<PracticeDoc>>> Handle(GetPracticeDocsQuery request, CancellationToken cancellationToken)
            {
                var data = await _practiceDocsRepository.GetPracticeDocsByPracticeId(request.PracticeId);
                var pagedData = Paginate(data, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<PracticeDoc>>(pagedData, data.Count(), request.PageNumber);

            }
        }
    }
}
