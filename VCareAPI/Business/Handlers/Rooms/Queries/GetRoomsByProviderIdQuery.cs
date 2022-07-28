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
    public class GetRoomsByProviderIdQuery : IRequest<IDataResult<IEnumerable<ProviderRoomDto>>>
    {
        public int? ProviderId { get; set; }

        public class GetRoomsByProviderIdQueryHandler : IRequestHandler<GetRoomsByProviderIdQuery, IDataResult<IEnumerable<ProviderRoomDto>>>
        {
            private readonly IRoomRepository _roomRepository;

            public GetRoomsByProviderIdQueryHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<ProviderRoomDto>>> Handle(GetRoomsByProviderIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _roomRepository.GetAllRoomsByProviderId(request.ProviderId);
                return new SuccessDataResult<IEnumerable<ProviderRoomDto>>(list.ToList());
            }
        }
    }
}
