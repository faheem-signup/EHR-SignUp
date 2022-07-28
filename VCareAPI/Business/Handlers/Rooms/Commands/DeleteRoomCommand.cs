using Business.BusinessAspects;
using Business.Constants;
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
namespace Business.Handlers.Rooms.Commands
{
    public class DeleteRoomCommand : IRequest<IResult>
    {
        public int RoomId { get; set; }
        public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, IResult>
        {
            private readonly IRoomRepository _roomRepository;

            public DeleteRoomCommandHandler(IRoomRepository roomRepository)
            {
                _roomRepository = roomRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
            {
                var roomToDelete = await _roomRepository.GetAsync(x => x.RoomId == request.RoomId && x.IsDeleted != true);
                roomToDelete.IsDeleted = true;
                _roomRepository.Update(roomToDelete);
                await _roomRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
