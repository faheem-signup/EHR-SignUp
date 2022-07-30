using Business.Handlers.BillingClaims.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
  public  class ValidatorBillingClaim : AbstractValidator<CreateBillingClaimCommand>
    {
        public ValidatorBillingClaim()
        {
            RuleFor(p => p.PatientName).NotEmpty().WithMessage("Please Enter PatientName!");
            RuleFor(p => p.InsuranceId).NotEmpty().WithMessage("Please Enter InsuranceId!");

            RuleFor(p => p.BilledStatusId).NotEqual(0).WithMessage("Please Select Billed Status!");
            RuleFor(p => p.BilledStatusId).NotNull().WithMessage("Please Select Billed Status!");
            RuleFor(p => p.CurrentStatusId).NotEqual(0).WithMessage("Please Select Current Status!");
            RuleFor(p => p.CurrentStatusId).NotNull().WithMessage("Please Select Current Status");

            RuleFor(p => p.PrimaryInsurance).NotEmpty().WithMessage("Please Enter Primary Insurance!");
            RuleFor(p => p.SecondaryInsurance).NotEmpty().WithMessage("Please Enter Secondary Insurance!");

            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.AttendingProvider).NotEmpty().WithMessage("Please Enter Attending Provider!");
            RuleFor(p => p.SupervisingProvider).NotEmpty().WithMessage("Please Enter Supervising Provider!");
            RuleFor(p => p.BillingProvider).NotEmpty().WithMessage("Please Enter Billing Provider!");
            RuleFor(p => p.PlaceOfService).NotEmpty().WithMessage("Please Enter Place of Service!");
            RuleFor(p => p.ICDCode1).NotEmpty().WithMessage("Please Enter ICD Code 1!");
        }
    }

    public class ValidatorUpdateBillingClaim : AbstractValidator<UpdateBillingClaimCommand>
    {
        public ValidatorUpdateBillingClaim()
        {
            RuleFor(p => p.BilledStatusId).NotEqual(0).WithMessage("Please Select Billed Status!");

            RuleFor(p => p.PatientName).NotEmpty().WithMessage("Please Enter PatientName!");
            RuleFor(p => p.InsuranceId).NotEmpty().WithMessage("Please Enter InsuranceId!");

            RuleFor(p => p.BilledStatusId).NotEqual(0).WithMessage("Please Select Billed Status!");
            RuleFor(p => p.BilledStatusId).NotNull().WithMessage("Please Select Billed Status!");
            RuleFor(p => p.CurrentStatusId).NotEqual(0).WithMessage("Please Select Current Status!");
            RuleFor(p => p.CurrentStatusId).NotNull().WithMessage("Please Select Current Status");

            RuleFor(p => p.PrimaryInsurance).NotEmpty().WithMessage("Please Enter Primary Insurance!");
            RuleFor(p => p.SecondaryInsurance).NotEmpty().WithMessage("Please Enter Secondary Insurance!");

            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.AttendingProvider).NotEmpty().WithMessage("Please Enter Attending Provider!");
            RuleFor(p => p.SupervisingProvider).NotEmpty().WithMessage("Please Enter Supervising Provider!");
            RuleFor(p => p.BillingProvider).NotEmpty().WithMessage("Please Enter Billing Provider!");
            RuleFor(p => p.PlaceOfService).NotEmpty().WithMessage("Please Enter Place of Service!");
            RuleFor(p => p.ICDCode1).NotEmpty().WithMessage("Please Enter ICD Code 1!");
        }
    }
}
