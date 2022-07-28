using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Appointments.Commands
{
    public class DeleteAppointmentCommand : IRequest<IResult>
    {
        public int AppointmentId { get; set; }
        public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, IResult>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IHttpContextAccessor _contextAccessor;
            public DeleteAppointmentCommandHandler(IAppointmentRepository AppointmentRepository, IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = AppointmentRepository;
                _contextAccessor = contextAccessor;
            }

          //  [SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
            {
                var existingAppointment = await _appointmentRepository.GetAsync(x => x.AppointmentId == request.AppointmentId);
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (existingAppointment != null)
                {
                    existingAppointment.IsDeleted = true;
                    existingAppointment.ModifiedBy = int.Parse(userId);
                    existingAppointment.ModifiedDate = DateTime.Now;
                }
                _appointmentRepository.Update(existingAppointment);
                await _appointmentRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
