using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers.Validators;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IRoomRepository;
using Entities.Concrete.RoomEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Rooms.Commands
{
    public class UpdateRoomCommand : IRequest<IResult>
    {
        public int RoomId { get; set; }
        public int? AreaId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, IResult>
        {
            private readonly IRoomRepository _roomRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateRoomCommandHandler(IRoomRepository roomRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _roomRepository = roomRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorUpdateRoom), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                if (request.TimeFrom != null && request.TimeTo != null)
                {
                    DateTime dateFrom = (DateTime)request.TimeFrom;
                    DateTime dateTo = (DateTime)request.TimeTo;
                    int result = DateTime.Compare(dateFrom, dateTo);
                    string relationship;

                    if (result >= 0)
                        return new ErrorResult(Messages.TimeShouldbeLessThen);
                }

                var roomObj = await _roomRepository.GetAsync(x => x.RoomId == request.RoomId && x.IsDeleted != true);
                if (roomObj != null)
                {
                    if (roomObj.RoomName != request.RoomName)
                    {
                        var roomName = await _roomRepository.GetAsync(x => x.RoomName == request.RoomName && x.IsDeleted != true);
                        if (roomName != null)
                        {
                            return new ErrorResult(Messages.UniqueName);
                        }
                    }

                    if (roomObj.RoomNumber != request.RoomNumber)
                    {
                        var roomNumber = await _roomRepository.GetAsync(x => x.RoomNumber == request.RoomNumber && x.IsDeleted != true);
                        if (roomNumber != null)
                        {
                            return new ErrorResult(Messages.UniqueRoomNumber);
                        }
                    }

                    roomObj.RoomId = request.RoomId;
                    roomObj.AreaId = request.AreaId;
                    roomObj.RoomNumber = request.RoomNumber;
                    roomObj.RoomName = request.RoomName;
                    roomObj.TimeFrom = request.TimeFrom;
                    roomObj.TimeTo = request.TimeTo;
                    roomObj.ModifiedBy = int.Parse(userId);
                    roomObj.ModifiedDate = DateTime.Now;

                    _roomRepository.Update(roomObj);
                    await _roomRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Updated);
                }
                else
                {
                    return new ErrorResult(Messages.NoRecordFound);
                }
            }
        }
    }
}
