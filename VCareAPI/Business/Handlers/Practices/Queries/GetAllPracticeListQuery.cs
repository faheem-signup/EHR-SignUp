using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IPracticesRepository;
using Entities.Concrete.PracticesEntity;
using Entities.Dtos.PracticeDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Practices.Queries
{
    public class GetAllPracticeListQuery : IRequest<IDataResult<IEnumerable<Practice>>>
    {
       
        public class GetAllPracticeListQueryHandler : IRequestHandler<GetAllPracticeListQuery, IDataResult<IEnumerable<Practice>>>
        {
            private readonly IPracticesRepository _practicesRepository;
            public GetAllPracticeListQueryHandler(IPracticesRepository practicesRepository)
            {
                _practicesRepository = practicesRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Practice>>> Handle(GetAllPracticeListQuery request, CancellationToken cancellationToken)
            {
                var practiceList = await _practicesRepository.GetListAsync();
                if (practiceList.Count() > 0)
                {
                    practiceList = practiceList.OrderByDescending(x => x.PracticeId).ToList();
                }

                return new SuccessDataResult<IEnumerable<Practice>>(practiceList.ToList());
            }
        }
    }
}
