using Business.Handlers.AppointmentReason.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorAppointmentReason : AbstractValidator<CreateAppointmentReasonCommand>
    {
        public ValidatorAppointmentReason()
        {
            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.AppointmentReasonDescription).NotEmpty().WithMessage("Please Enter Reason!");
        }
    }

    public class ValidatorUpdateAppointmentReason : AbstractValidator<UpdateAppointmentReasonCommand>
    {
        public ValidatorUpdateAppointmentReason()
        {
            RuleFor(p => p.AppointmentReasonId).NotEqual(0).WithMessage("Please Enter AppointmentReasonId!");
            RuleFor(p => p.AppointmentReasonId).NotEqual(0).WithMessage("Please Enter AppointmentReasonId!");
            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.AppointmentReasonDescription).NotEmpty().WithMessage("Please Enter Reason!");
        }
    }
}
