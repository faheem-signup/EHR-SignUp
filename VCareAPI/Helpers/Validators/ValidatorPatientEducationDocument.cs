using Business.Handlers.PatientEducation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientEducationDocument : AbstractValidator<CreatePatientEducationDocumentCommand>
    {
        public ValidatorPatientEducationDocument()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.ICDCode).MaximumLength(200).WithMessage("Please Enter ICD Code Less Than 200 Characters!");
        }
    }
}
