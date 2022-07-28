using Business.Handlers.PatientInsuranceAuthorizations.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientInsuranceAuthorization : AbstractValidator<CreatePatientInsuranceAuthorizationCommand>
    {
        public ValidatorPatientInsuranceAuthorization()
        {
            RuleFor(p => p.PatientInsuranceId).NotEqual(0).WithMessage("Please Select Insurance!");
            RuleFor(p => p.PatientInsuranceId).NotNull().WithMessage("Please Select Insurance!");
            RuleFor(p => p.AuthorizationNo).NotEmpty().WithMessage("Please Enter Authorization No.!");
            RuleFor(p => p.Count).NotEmpty().WithMessage("Please Enter Count!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Select Start Date!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Select End Date!");
        }
    }

    public class ValidatorUpdatePatientInsuranceAuthorization : AbstractValidator<UpdatePatientInsuranceAuthorizationCommand>
    {
        public ValidatorUpdatePatientInsuranceAuthorization()
        {
            RuleFor(p => p.PatientInsuranceAuthorizationId).NotEqual(0).WithMessage("Please Select AuthorizationId!");
            RuleFor(p => p.PatientInsuranceAuthorizationId).NotNull().WithMessage("Please Select AuthorizationId!");
            RuleFor(p => p.PatientInsuranceId).NotEqual(0).WithMessage("Please Select Insurance!");
            RuleFor(p => p.PatientInsuranceId).NotNull().WithMessage("Please Select Insurance!");
            RuleFor(p => p.AuthorizationNo).NotEmpty().WithMessage("Please Enter Authorization No.!");
            RuleFor(p => p.Count).NotEmpty().WithMessage("Please Enter Count!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Select Start Date!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Select End Date!");
        }
    }
}
