
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Concrete;
using System;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.Http;
using System.Linq;
using DataAccess.Abstract.IAppointmentReasonsRepository;
using Entities.Concrete.AppointmentReasonsEntity;

namespace Business.Handlers.AppointmentReason.Commands
{
   

    public class CreateAppointmentReasonCommand : IRequest<IResult>
    {
        public string AppointmentReasonDescription { get; set; }
        public int? LocationId { get; set; }
        public class CreateAppointmentReasonCommandHandler : IRequestHandler<CreateAppointmentReasonCommand, IResult>
        {
            private readonly IAppointmentReasonsRepository _appointmentReasonsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public CreateAppointmentReasonCommandHandler(IAppointmentReasonsRepository appointmentReasonsRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _appointmentReasonsRepository = appointmentReasonsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAppointmentReasonCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                AppointmentReasons appointmentReasonsObj = new AppointmentReasons 
                {
                    AppointmentReasonDescription = request.AppointmentReasonDescription,
                    LocationId = request.LocationId,
                    CreatedBy = int.Parse(userId),
                    CreatedDate = DateTime.Now,
                    ModifiedBy = int.Parse(userId),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };

                _appointmentReasonsRepository.Add(appointmentReasonsObj);
                await _appointmentReasonsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}

