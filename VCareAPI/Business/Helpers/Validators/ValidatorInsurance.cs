using Business.Handlers.Insurances.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorInsurance : AbstractValidator<CreateInsurancesCommand>
    {
        public ValidatorInsurance()
        {
            RuleFor(p => p.PayerId).NotEqual(0).WithMessage("Please Enter Payer Id");
            RuleFor(p => p.PayerId).NotNull().WithMessage("Please Enter Payer Id");
            RuleFor(p => p.InsurancePayerTypeId).NotEqual(0).WithMessage("Please select Payer Type");
            RuleFor(p => p.InsurancePayerTypeId).NotNull().WithMessage("Please select Payer Type");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please Enter Code!");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please select City");
            //RuleFor(p => p.City).NotNull().WithMessage("Please select City");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email!");

        }
    }

    public class ValidatorUpdateInsurance : AbstractValidator<UpdateInsurancesCommand>
    {
        public ValidatorUpdateInsurance()
        {
            RuleFor(p => p.InsuranceId).NotEqual(0).WithMessage("Please Enter InsuranceId");
            RuleFor(p => p.InsuranceId).NotNull().WithMessage("Please Enter InsuranceId");
            RuleFor(p => p.PayerId).NotEqual(0).WithMessage("Please Enter Payer Id");
            RuleFor(p => p.PayerId).NotNull().WithMessage("Please Enter Payer Id");
            RuleFor(p => p.InsurancePayerTypeId).NotEqual(0).WithMessage("Please select Payer Type");
            RuleFor(p => p.InsurancePayerTypeId).NotNull().WithMessage("Please select Payer Type");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please Enter Code!");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please select City");
            //RuleFor(p => p.City).NotNull().WithMessage("Please select City");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email!");
        }
    }
}
