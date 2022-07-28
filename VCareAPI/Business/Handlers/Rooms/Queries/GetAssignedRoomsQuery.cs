using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoomRepository;
using Entities.Concrete.RoomEntity;
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
    public class GetAssignedRoomsQuery : BasePaginationQuery<IDataResult<IEnumerable<AssignedRoomDto>>>
    {
        public class GetAssignedRoomsQueryHandler : IRequestHandler<GetAssignedRoomsQuery, IDataResult<IEnumerable<AssignedRoomDto>>>
        {
            private readonly IRoomRepository _roomRepository;
            public GetAssignedRoomsQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AssignedRoomDto>>> Handle(GetAssignedRoomsQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetAssignedRoomList();

                return new PagedDataResult<IEnumerable<AssignedRoomDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
