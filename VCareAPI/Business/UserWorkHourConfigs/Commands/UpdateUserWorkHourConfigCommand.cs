using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
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
    public class UpdateUserWorkHourConfigCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int? LocationId { get; set; }
        public string Days { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime SecondShiftWorkFrom { get; set; }
        public DateTime SecondShiftWorkTo { get; set; }
        public int? UserId { get; set; }
        public class UpdateUserWorkHourConfigCommandHandler : IRequestHandler<UpdateUserWorkHourConfigCommand, IResult>
        {
            private readonly IUserWorkHourRepository _userWorkHourRepository;

            public UpdateUserWorkHourConfigCommandHandler(IUserWorkHourRepository userWorkHourRepository)
            {
                _userWorkHourRepository = userWorkHourRepository;
            }

           // [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserWorkHourConfigCommand request, CancellationToken cancellationToken)
            {
                UserWorkHour userWorkHourObj = new UserWorkHour
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    LocationId = request.LocationId,
                    Days = request.Days,
                    FirstShiftWorkFrom = request.FirstShiftWorkFrom,
                    FirstShiftWorkTo = request.FirstShiftWorkTo,
                    SecondShiftWorkFrom = request.SecondShiftWorkFrom,
                    SecondShiftWorkTo = request.SecondShiftWorkTo,

                };

                _userWorkHourRepository.Update(userWorkHourObj);
                await _userWorkHourRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
