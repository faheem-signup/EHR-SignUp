using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoomRepository;
using Entities.Concrete.RoomEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Rooms.Queries
{
    public class GetRoomByIdQuery : IRequest<IDataResult<Room>>
    {
        public int RoomId { get; set; }
        public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, IDataResult<Room>>
        {
            private readonly IRoomRepository _roomRepository;

            public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Room>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
            {
                var room = await _roomRepository.GetAsync(x => x.RoomId == request.RoomId && x.IsDeleted != true);

                return new SuccessDataResult<Room>(room);
            }
        }
    }
}
