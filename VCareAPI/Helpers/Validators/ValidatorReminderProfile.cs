namespace Business.Helpers.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Business.Handlers.ReminderProfiles.Commands;
    using FluentValidation;

    public class ValidatorReminderProfile : AbstractValidator<CreateReminderProfileCommand>
    {
        public ValidatorReminderProfile()
        {
            RuleFor(p => p.ReminderTypeId).NotEqual(0).WithMessage("Please Select Type!");
            RuleFor(p => p.ReminderTypeId).NotNull().WithMessage("Please Select Type!");
            RuleFor(p => p.Count).NotEqual(0).WithMessage("Please Enter Count!");
            RuleFor(p => p.Count).NotNull().WithMessage("Please Enter Count!");
            RuleFor(p => p.ReminderDaysLookupId).NotEqual(0).WithMessage("Please Select Date !");
            RuleFor(p => p.ReminderDaysLookupId).NotNull().WithMessage("Please Select Date !");
        }
    }

    public class ValidatorUpdateReminderProfile : AbstractValidator<UpdateReminderProfileCommand>
    {
        public ValidatorUpdateReminderProfile()
        {
            RuleFor(p => p.ReminderProfileId).NotEqual(0).WithMessage("Please Enter ReminderProfileId!");
            RuleFor(p => p.ReminderProfileId).NotNull().WithMessage("Please Enter ReminderProfileId!");
            RuleFor(p => p.ReminderTypeId).NotEqual(0).WithMessage("Please Select Type!");
            RuleFor(p => p.ReminderTypeId).NotNull().WithMessage("Please Select Type!");
            RuleFor(p => p.Count).NotEmpty().WithMessage("Please Select Count!");
            RuleFor(p => p.ReminderDaysLookupId).NotEqual(0).WithMessage("Please Select Date !");
            RuleFor(p => p.ReminderDaysLookupId).NotNull().WithMessage("Please Select Date !");
        }
    }
}
