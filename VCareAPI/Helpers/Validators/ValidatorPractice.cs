using Business.Handlers.Practices.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
   public class ValidatorPractice : AbstractValidator<CreatePracticeCommand>
    {
       public ValidatorPractice()
        {
            RuleFor(p => p.PracticeTypeId).NotEqual(0).WithMessage("Please Select Type !");
            RuleFor(p => p.PracticeTypeId).NotNull().WithMessage("Please Select Type !");
            RuleFor(p => p.LegalBusinessName).NotEmpty().WithMessage("Please Enter Legal Business Name !");
            RuleFor(p => p.LegalBusinessName).NotEmpty().Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please Enter Alpha numeric characters only");
            RuleFor(p => p.PhysicalAddress).NotEmpty().WithMessage("Please Enter Physical Address!");
            RuleFor(p => p.PhysicalAddress).MaximumLength(50).WithMessage("Please Enter Phone No Maximum 200 Chracters");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Enter City !");
            RuleFor(p => p.City).NotNull().WithMessage("Please Enter City !");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter State !");
            RuleFor(p => p.State).NotNull().WithMessage("Please Enter State !");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Zip !");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Zip !");
            //RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Please Enter Phone No. !");
            RuleFor(p => p.PhoneNumber).MaximumLength(50).WithMessage("Please Enter Phone No Maximum 50 Chracters");
            //RuleFor(p => p.OfficeEmail).NotEmpty().WithMessage("Please Enter Office Email !").EmailAddress().WithMessage("A valid Office Email is required");
            RuleFor(p => p.BillingAddress).NotEmpty().WithMessage("Please Enter Billing Address !");
            RuleFor(p => p.PhysicalAddress).NotEmpty().Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please Enter Alpha numeric characters only");
            RuleFor(p => p.BillingCity).NotEqual(0).WithMessage("Please Enter Billing City !");
            RuleFor(p => p.BillingCity).NotNull().WithMessage("Please Enter Billing City !");
            RuleFor(p => p.BillingState).NotEqual(0).WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingState).NotNull().WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingZIP).NotEqual(0).WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingZIP).NotNull().WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingNPI).NotEmpty().WithMessage("Please Enter Npi !");
            //RuleFor(p => p.TaxTypeId).NotEqual(0).WithMessage("Please Select Tax ID Type !");
            //RuleFor(p => p.TaxTypeId).NotNull().WithMessage("Please Select Tax ID Type !");
            //RuleFor(p => p.AcceptAssignment).NotEqual(0).WithMessage("Please Select Assignment !");
            //RuleFor(p => p.AcceptAssignment).NotNull().WithMessage("Please Select Assignment !");
            //RuleFor(p => p.Taxonomy).NotEmpty().WithMessage("Please Enter Taxonomy !");
        }
    }

    public class ValidatorUpdatePractice : AbstractValidator<UpdatePracticeCommand>
    {
        public ValidatorUpdatePractice()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please Enter PracticeId !");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please Enter PracticeId !");
            RuleFor(p => p.PracticeTypeId).NotEqual(0).WithMessage("Please Select Type !");
            RuleFor(p => p.PracticeTypeId).NotNull().WithMessage("Please Select Type !");
            RuleFor(p => p.LegalBusinessName).NotEmpty().WithMessage("Please Enter Legal Business Name !");
            RuleFor(p => p.LegalBusinessName).NotEmpty().Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please Enter Alpha numeric characters only");
            RuleFor(p => p.PhysicalAddress).NotEmpty().WithMessage("Please Enter Physical Address!");
            RuleFor(p => p.PhysicalAddress).NotEmpty().Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please Enter Alpha numeric characters only");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Enter City !");
            RuleFor(p => p.City).NotNull().WithMessage("Please Enter City !");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter State !");
            RuleFor(p => p.State).NotNull().WithMessage("Please Enter State !");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Zip !");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Zip !");
            RuleFor(p => p.PhoneNumber).MaximumLength(50).WithMessage("Please Enter Phone No Maximum 50 Chracters");
            //RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Please Enter Phone No. !");
            //RuleFor(p => p.OfficeEmail).NotEmpty().WithMessage("Please Enter Office Email !").EmailAddress().WithMessage("A valid Office Email is required");
            RuleFor(p => p.BillingAddress).NotEmpty().WithMessage("Please Enter Billing Address !");
            RuleFor(p => p.BillingCity).NotEqual(0).WithMessage("Please Enter Billing City !");
            RuleFor(p => p.BillingCity).NotNull().WithMessage("Please Enter Billing City !");
            RuleFor(p => p.BillingState).NotEqual(0).WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingState).NotNull().WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingZIP).NotEqual(0).WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingZIP).NotNull().WithMessage("Please Enter Billing State !");
            RuleFor(p => p.BillingNPI).NotEmpty().WithMessage("Please Enter Npi !");
            //RuleFor(p => p.TaxTypeId).NotEqual(0).WithMessage("Please Select Tax ID Type !");
            //RuleFor(p => p.TaxTypeId).NotNull().WithMessage("Please Select Tax ID Type !");
            //RuleFor(p => p.AcceptAssignment).NotEqual(0).WithMessage("Please Select Assignment !");
            //RuleFor(p => p.AcceptAssignment).NotNull().WithMessage("Please Select Assignment !");
            //RuleFor(p => p.Taxonomy).NotEmpty().WithMessage("Please Enter Taxonomy !");
        }
    }
}
