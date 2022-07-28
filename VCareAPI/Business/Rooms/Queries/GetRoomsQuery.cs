using Business.BusinessAspects;
using Business.Helpers.BasePager;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoomRepository;
using Entities.Concrete.RoomEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Rooms.Queries
{
    public class GetRoomsQuery : BasePaginationQuery<IDataResult<IEnumerable<Room>>>
    {
        public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IDataResult<IEnumerable<Room>>>
        {
            private readonly IRoomRepository _roomRepository;

            public GetRoomsQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Room>>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetListAsync(x => x.IsDeleted != true);
                if (list.Count() > 0)
                {
                    list = list.OrderByDescending(x => x.RoomId).ToList();
                }

                var pagedData = Paginate(list, request.PageNumber, request.PageSize);
                return new PagedDataResult<IEnumerable<Room>>(pagedData, list.Count(), request.PageNumber);
            }
        }
    }
}
