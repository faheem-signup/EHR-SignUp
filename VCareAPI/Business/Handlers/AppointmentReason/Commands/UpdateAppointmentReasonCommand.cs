using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentReasonsRepository;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentReason.Commands
{
    public class UpdateAppointmentReasonCommand : IRequest<IResult>
    {
        public int AppointmentReasonId { get; set; }
        public string AppointmentReasonDescription { get; set; }
        public int? LocationId { get; set; }
        public class UpdateAppointmentReasonCommandHandler : IRequestHandler<UpdateAppointmentReasonCommand, IResult>
        {
            private readonly IAppointmentReasonsRepository _appointmentReasonsRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;

            public UpdateAppointmentReasonCommandHandler(IAppointmentReasonsRepository appointmentReasonsRepository, IMediator mediator, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _appointmentReasonsRepository = appointmentReasonsRepository;
                _mediator = mediator;
                _mapper = mapper;
                _contextAccessor = contextAccessor;
            }

            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAppointmentReasonCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var existingappointmentReasons = await _appointmentReasonsRepository.GetAsync(x => x.AppointmentReasonId == request.AppointmentReasonId && x.IsDeleted != true);

                if(existingappointmentReasons != null)
                {
                    existingappointmentReasons.AppointmentReasonDescription = request.AppointmentReasonDescription;
                    existingappointmentReasons.LocationId = request.LocationId;
                    existingappointmentReasons.ModifiedBy = int.Parse(userId);
                    existingappointmentReasons.ModifiedDate = DateTime.Now;
                    _appointmentReasonsRepository.Update(existingappointmentReasons);
                    await _appointmentReasonsRepository.SaveChangesAsync();
                }

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
