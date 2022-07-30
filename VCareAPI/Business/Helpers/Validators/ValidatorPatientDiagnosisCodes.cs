using Business.Handlers.PatientDiagnosisCodes.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientDiagnosisCodes : AbstractValidator<CreatePatientDiagnosisCodeCommand>
    {
        public ValidatorPatientDiagnosisCodes()
        {
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select Provider!");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select Provider!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.DiagnosisId).NotEqual(0).WithMessage("Please Enter Diagnosis Code!");
            RuleFor(p => p.DiagnosisId).NotNull().WithMessage("Please Enter Diagnosis Code!");
            RuleFor(p => p.DiagnosisDate).NotEmpty().WithMessage("Please Select Diagnosis Date!");
            RuleFor(p => p.ResolvedDate).NotEmpty().WithMessage("Please Select Resolved Date!");
            RuleFor(p => p.SNOMEDCode).NotEmpty().WithMessage("Please Enter SNOMED Code!");
            RuleFor(p => p.SNOMEDCodeDesctiption).NotEmpty().WithMessage("Please Enter SNOMED Description!");
            RuleFor(p => p.DiagnoseCodeType).MaximumLength(50).WithMessage("Please Enter Diagnose Code Type Less Than 50 Characters!");
            RuleFor(p => p.SNOMEDCode).MaximumLength(50).WithMessage("Please Enter SNOMED Code Less Than 50 Characters!");
            RuleFor(p => p.SNOMEDCodeDesctiption).MaximumLength(200).WithMessage("Please Enter SNOMED Code Desctiption Less Than 10 Characters!");
            RuleFor(p => p.Description).MaximumLength(200).WithMessage("Please Enter Description Less Than 200 Characters!");
            RuleFor(p => p.Comments).MaximumLength(200).WithMessage("Please Enter Comments Less Than 200 Characters!");
        }
    }
}
