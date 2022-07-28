using AutoMapper;
using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPatientRepository;
using DataAccess.Abstract.IICDToPracticesRepository;
using Entities.Concrete.PatientEntity;
using Entities.Concrete.ICDToPracticesEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Dtos.PracticeDto;

namespace Business.Handlers.ICDToPractice.Queries
{
    public class GetICDToPracticesListQuery : BasePaginationQuery<IDataResult<IEnumerable<ICDToPractices>>>
    {
        public int PracticeId { get; set; }

        public class GetICDToPracticesListQueryHandler : IRequestHandler<GetICDToPracticesListQuery, IDataResult<IEnumerable<ICDToPractices>>>
        {
            private readonly IICDToPracticesRepository _icdToPracticesRepository;
            private readonly IMapper _mapper;

            public GetICDToPracticesListQueryHandler(IICDToPracticesRepository icdToPracticesRepository, IMapper mapper)
            {
                _icdToPracticesRepository = icdToPracticesRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ICDToPractices>>> Handle(GetICDToPracticesListQuery request, CancellationToken cancellationToken)
            {
                var list = await _icdToPracticesRepository.GetListAsync(x => x.PracticeId == request.PracticeId);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.ICDToPracticesId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<ICDToPractices>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
