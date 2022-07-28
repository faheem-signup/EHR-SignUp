using Business.Handlers.PatientEducation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientEducationWeb : AbstractValidator<CreatePatientEducationWebCommand>
    {
        public ValidatorPatientEducationWeb()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.Title).MaximumLength(200).WithMessage("Please Enter Title Less Than 200 Characters!");
            RuleFor(p => p.WebUrl).MaximumLength(200).WithMessage("Please Enter Web Url Less Than 200 Characters!");
        }
    }
}
