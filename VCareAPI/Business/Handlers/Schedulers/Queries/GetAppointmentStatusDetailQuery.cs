using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.ISchedulerRepository;
using Entities.Dtos.SchedulerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Schedulers.Queries
{
    public class GetAppointmentStatusDetailQuery : IRequest<IDataResult<IEnumerable<AppiontmentStatusDetailDto>>>
    {
        public int? ProviderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AppointmentStatus { get; set; }

        public class GetAppointmentStatusDetailQueryHandler : IRequestHandler<GetAppointmentStatusDetailQuery, IDataResult<IEnumerable<AppiontmentStatusDetailDto>>>
        {
            private readonly ISchedulerRepository _schedulerRepository;

            public GetAppointmentStatusDetailQueryHandler(ISchedulerRepository schedulerRepository)
            {
                _schedulerRepository = schedulerRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AppiontmentStatusDetailDto>>> Handle(GetAppointmentStatusDetailQuery request, CancellationToken cancellationToken)
            {
                var list = await _schedulerRepository.GetAppointmentStatusDetailById(request.ProviderId, request.FromDate, request.ToDate, request.AppointmentStatus);
                return new SuccessDataResult<IEnumerable<AppiontmentStatusDetailDto>>(list.ToList());
            }
        }
    }
}
