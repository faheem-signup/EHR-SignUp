using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract.IAppointmentRepository;
using DataAccess.Abstract.IRecurringAppointmentsRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RecurringAppointments.Commands
{
    public class DeleteRecurringAppointmentCommand : IRequest<IResult>
    {
        public int RecurringAppointmentId { get; set; }

        public class DeleteReferralProviderCommandHandler : IRequestHandler<DeleteRecurringAppointmentCommand, IResult>
        {
            private readonly IRecurringAppointmentsRepository _recurringAppointmentsRepository;
            private readonly IAppointmentRepository _appointmentRepository;

            public DeleteReferralProviderCommandHandler(IRecurringAppointmentsRepository recurringAppointmentsRepository, IAppointmentRepository appointmentRepository)
            {
                _recurringAppointmentsRepository = recurringAppointmentsRepository;
                _appointmentRepository = appointmentRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteRecurringAppointmentCommand request, CancellationToken cancellationToken)
            {
                var recurringAppointmentsObj = await _recurringAppointmentsRepository.GetAsync(x => x.RecurringAppointmentId == request.RecurringAppointmentId);
                if (recurringAppointmentsObj != null)
                {
                    var appointmentObj = await _appointmentRepository.GetAsync(x => x.AppointmentId == recurringAppointmentsObj.AppointmentId && x.IsDeleted != true);

                    if (appointmentObj != null)
                    {
                        appointmentObj.IsDeleted = true;
                        _appointmentRepository.Update(appointmentObj);
                        await _appointmentRepository.SaveChangesAsync();

                        return new SuccessResult(Messages.Deleted);
                    }
                    else
                    {
                        return new SuccessResult(Messages.NoRecordFound);
                    }
                }
                else
                {
                    return new SuccessResult(Messages.NoRecordFound);
                }
            }
        }
    }
}
