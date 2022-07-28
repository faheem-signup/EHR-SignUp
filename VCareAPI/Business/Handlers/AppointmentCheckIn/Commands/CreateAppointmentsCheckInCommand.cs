using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IAppointmentsCheckInRepository;
using DataAccess.Abstract.IFollowUpAppointmentRepository;
using DataAccess.Abstract.IGroupPatientAppointmentRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using Entities.Concrete.AppointmentEntity;
using Entities.Concrete.AppointmentsCheckInEntity;
using Entities.Concrete.FollowUpAppointmentEntity;
using Entities.Concrete.GroupPatientAppointmentEntity;
using Entities.Concrete.RecurringAppointmentsEntity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AppointmentCheckIn.Commands
{

    public class CreateAppointmentsCheckInCommand : IRequest<IResult>
    {
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
        public DateTime? AppointmentCheckInDateTime { get; set; }
        //public DateTime? AppointmentCheckOutDateTime { get; set; }
        public class CreateAppointmentsCheckInCommandHandler : IRequestHandler<CreateAppointmentsCheckInCommand, IResult>
        {
            private readonly IAppointmentsCheckInRepository _appointmentCheckInRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public CreateAppointmentsCheckInCommandHandler(IAppointmentsCheckInRepository appointmentCheckInRepository, IMediator mediator, IMapper mapper)
            {
                _appointmentCheckInRepository = appointmentCheckInRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
           // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAppointmentsCheckInCommand request, CancellationToken cancellationToken)
            {
                AppointmentsCheckIn appointmentCheckInObj = new AppointmentsCheckIn
                {
                    AppointmentId = request.AppointmentId,
                    AppointmentStatusId = request.AppointmentStatusId,
                    AppointmentCheckInDateTime = request.AppointmentCheckInDateTime,
                    //AppointmentCheckOutDateTime = request.AppointmentCheckOutDateTime,
                };

                _appointmentCheckInRepository.Add(appointmentCheckInObj);
                await _appointmentCheckInRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}