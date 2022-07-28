using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IUserWorkHourConfigRepository;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserWorkHourConfigs.Commands
{

    public class CreateUserWorkHourConfigCommand : IRequest<IResult>
    {
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime SecondShiftWorkFrom { get; set; }
        public DateTime SecondShiftWorkTo { get; set; }
        public int? UserId { get; set; }
        public class CreateUserWorkHourConfigCommandHandler : IRequestHandler<CreateUserWorkHourConfigCommand, IResult>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IUserWorkHourRepository _userWorkHourRepository;

            public CreateUserWorkHourConfigCommandHandler(IUserWorkHourRepository userWorkHourRepository,IMediator mediator, IMapper mapper)
            {
                _userWorkHourRepository = userWorkHourRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateUserWorkHourConfigCommand request, CancellationToken cancellationToken)
            {

                UserWorkHour userWorkHourObj = new UserWorkHour
                {
                    UserId = request.UserId,
                    LocationId = request.LocationId,
                    Days = request.Days,
                    FirstShiftWorkFrom = request.FirstShiftWorkFrom,
                    FirstShiftWorkTo = request.FirstShiftWorkTo,
                    SecondShiftWorkFrom = request.SecondShiftWorkFrom,
                    SecondShiftWorkTo = request.SecondShiftWorkTo,

                };

                _userWorkHourRepository.Add(userWorkHourObj);
                await _userWorkHourRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}

