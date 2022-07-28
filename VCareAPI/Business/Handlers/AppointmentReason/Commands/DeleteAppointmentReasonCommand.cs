using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentReasonsRepository;
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
    public class DeleteAppointmentReasonCommand : IRequest<IResult>
    {
        public int AppointmentReasonId { get; set; }

        public class DeleteAppointmentReasonCommandHandler : IRequestHandler<DeleteAppointmentReasonCommand, IResult>
        {
            private readonly IAppointmentReasonsRepository _appointmentReasonsRepository;
            private readonly IHttpContextAccessor _contextAccessor;

            public DeleteAppointmentReasonCommandHandler(IAppointmentReasonsRepository appointmentReasonsRepository, IHttpContextAccessor contextAccessor)
            {
                _appointmentReasonsRepository = appointmentReasonsRepository;
                _contextAccessor= contextAccessor;
            }

            //[SecuredOperation(Priority = 1)]
            //[CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteAppointmentReasonCommand request, CancellationToken cancellationToken)
            {
                var userId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                var existingappointmentReasons = await _appointmentReasonsRepository.GetAsync(x => x.AppointmentReasonId == request.AppointmentReasonId);
                existingappointmentReasons.IsDeleted = true;
                existingappointmentReasons.ModifiedBy = int.Parse(userId);
                existingappointmentReasons.ModifiedDate = DateTime.Now;

                _appointmentReasonsRepository.Update(existingappointmentReasons);
                await _appointmentReasonsRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
