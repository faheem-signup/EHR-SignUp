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
    public class CreateRoomCommand : IRequest<IResult>
    {
        public int AreaId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, IResult>
        {
            private readonly IRoomRepository _roomRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateRoomCommandHandler(IRoomRepository roomRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _roomRepository = roomRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [ValidationAspect(typeof(ValidatorRoom), Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                if (request.TimeFrom != null && request.TimeTo != null)
                {
                    DateTime dateFrom = (DateTime)request.TimeFrom;
                    DateTime dateTo = (DateTime)request.TimeTo;
                    int result = DateTime.Compare(dateFrom, dateTo);

                    if (result >= 0)
                    {
                        return new ErrorResult(Messages.TimeShouldbeLessThen);
                    }
                }

                var roomName = await _roomRepository.GetAsync(x => x.RoomName == request.RoomName && x.IsDeleted != true);
                if (roomName != null)
                {
                    return new ErrorResult(Messages.UniqueName);
                }

                var roomNumber = await _roomRepository.GetAsync(x => x.RoomNumber == request.RoomNumber && x.IsDeleted != true);
                if (roomNumber != null)
                {
                    return new ErrorResult(Messages.UniqueRoomNumber);
                }

                Room roomObj = new Room
                {
                    AreaId = request.AreaId,
                    RoomNumber = request.RoomNumber,
                    RoomName = request.RoomName,
                    TimeFrom = request.TimeFrom,
                    TimeTo = request.TimeTo,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                };

                _roomRepository.Add(roomObj);
                await _roomRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
