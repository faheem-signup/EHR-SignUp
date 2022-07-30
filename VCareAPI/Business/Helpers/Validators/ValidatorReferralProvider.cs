using Business.Handlers.Providers.Commands;
using Business.Handlers.ReferralProviders.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorReferralProvider : AbstractValidator<CreateReferralProviderCommand>
    {
        public ValidatorReferralProvider()
        {
            RuleFor(p => p.ReferralProviderType).NotEmpty().WithMessage("Please Enter Referral Type!");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter First Name!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter Last Name!");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Address!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select City!");
            RuleFor(p => p.City).NotNull().WithMessage("Please Select City!");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Select State!");
            RuleFor(p => p.State).NotNull().WithMessage("Please Select State!");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Select ZIP!");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Select ZIP!");
            RuleFor(p => p.NPI).NotNull().WithMessage("Please Enter NPI!");
        }
    }

    public class ValidatorUpdateReferralProvider : AbstractValidator<UpdateReferralProviderCommand>
    {
        public ValidatorUpdateReferralProvider()
        {
            RuleFor(p => p.ReferralProviderId).NotEqual(0).WithMessage("Please Enter ReferralProviderId!");
            RuleFor(p => p.ReferralProviderId).NotNull().WithMessage("Please Enter ReferralProviderId!");
            RuleFor(p => p.ReferralProviderType).NotEmpty().WithMessage("Please Enter Referral Type!");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter First Name!");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Address!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select City!");
            RuleFor(p => p.City).NotNull().WithMessage("Please Select City!");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Select State!");
            RuleFor(p => p.State).NotNull().WithMessage("Please Select State!");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Select ZIP!");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Select ZIP!");
            RuleFor(p => p.NPI).NotNull().WithMessage("Please Enter NPI!");

        }
    }
}
