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
    public class GetAvailableRoomsQuery : BasePaginationQuery<IDataResult<IEnumerable<AvialableRoomDto>>>
    {
        public int? RoomId { get; set; }

        public DateTime? Date { get; set; }

        public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, IDataResult<IEnumerable<AvialableRoomDto>>>
        {
            private readonly IRoomRepository _roomRepository;

            public GetAvailableRoomsQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<AvialableRoomDto>>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetAvailableRoomsList(request.RoomId, request.Date);
                return new PagedDataResult<IEnumerable<AvialableRoomDto>>(list, list.Count(), request.PageNumber);
            }
        }
    }
}
