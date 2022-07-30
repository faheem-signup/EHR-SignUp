using Business.Handlers.PatientInsurances.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientInsurance : AbstractValidator<CreatePatientInsuranceCommand>
    {
        public ValidatorPatientInsurance()
        {
            RuleFor(p => p.InsuranceName).NotEmpty().WithMessage("Please Enter Insurance Name!");
            //RuleFor(p => p.EligibilityDate).NotEmpty().WithMessage("Please Select Eligibility Date!");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select Insurance City!");
            //RuleFor(p => p.City).NotNull().WithMessage("Please Select Insurance City!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Select Start Date!");
            //RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Select End Date!");
            When(x => x.InsuranceTypeName != "Rx Insurance", () =>
            {
                RuleFor(p => p.PolicyNumber).NotEmpty().WithMessage("Please Enter Policy Number!");
            });
        }
    }

    public class ValidatorUpdatePatientInsurance : AbstractValidator<UpdatePatientInsuranceCommand>
    {
        public ValidatorUpdatePatientInsurance()
        {
            RuleFor(p => p.PatientInsuranceId).NotEqual(0).WithMessage("Please Enter PatientInsuranceId!");
            RuleFor(p => p.PatientInsuranceId).NotNull().WithMessage("Please Enter PatientInsuranceId!");
            RuleFor(p => p.InsuranceName).NotEmpty().WithMessage("Please Enter Insurance Name!");
            //RuleFor(p => p.EligibilityDate).NotEmpty().WithMessage("Please Select Eligibility Date!");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select Insurance City!");
            //RuleFor(p => p.City).NotNull().WithMessage("Please Select Insurance City!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Select Start Date!");
            //RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Select End Date!");
            When(x => x.InsuranceTypeName != "Rx Insurance", () =>
            {
                RuleFor(p => p.PolicyNumber).NotEmpty().WithMessage("Please Enter Policy Number!");
            });
        }
    }
}
