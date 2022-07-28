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
    public class GetRoomPatientListQuery : IRequest<IDataResult<IEnumerable<RoomPatientDto>>>
    {
        public int? ProviderId { get; set; }
        public class GetRoomPatientListQueryHandler : IRequestHandler<GetRoomPatientListQuery, IDataResult<IEnumerable<RoomPatientDto>>>
        {
            private readonly IRoomRepository _roomRepository;
            public GetRoomPatientListQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<RoomPatientDto>>> Handle(GetRoomPatientListQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetRoomPatientList(request.ProviderId);
                return new SuccessDataResult<IEnumerable<RoomPatientDto>>(list.ToList());
            }
        }
    }
}
