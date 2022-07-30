using Business.Handlers.Patients.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientProvideReferring : AbstractValidator<CreatePatientProvideReferringCommand>
    {
        public ValidatorPatientProvideReferring()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select PatientId!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select PatientId!");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please Enter Name!");
            RuleFor(p => p.Name).MaximumLength(30).WithMessage("Please Enter Name Less Than 30 Characters!");
            RuleFor(p => p.Address).MaximumLength(200).WithMessage("Please Enter Address Less Than 200 Characters!");
            RuleFor(p => p.CellPhone).MaximumLength(20).WithMessage("Please Enter Cell Phone Less Than 20 Characters!");
            RuleFor(p => p.Fax).MaximumLength(20).WithMessage("Please Enter Fax Less Than 20 Characters!");
            RuleFor(p => p.ContactPerson).MaximumLength(20).WithMessage("Please Enter Contact Person Less Than 20 Characters!");
            RuleFor(p => p.NPI).MaximumLength(10).WithMessage("Please Enter NPI Less Than 10 Characters!");
        }
    }
}
