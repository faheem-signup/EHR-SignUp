using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoomRepository;
using Entities.Dtos.RoomDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Rooms.Queries
{
    public class GetRoomPatientCountQuery : IRequest<IDataResult<IEnumerable<RoomPatientCountDto>>>
    {
        public int? ProviderId { get; set; }
        public class GetRoomPatientCountQueryHandler : IRequestHandler<GetRoomPatientCountQuery, IDataResult<IEnumerable<RoomPatientCountDto>>>
        {
            private readonly IRoomRepository _roomRepository;
            public GetRoomPatientCountQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<RoomPatientCountDto>>> Handle(GetRoomPatientCountQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetRoomPatientCountList(request.ProviderId);
                return new SuccessDataResult<IEnumerable<RoomPatientCountDto>>(list.ToList());
            }
        }
    }
}
