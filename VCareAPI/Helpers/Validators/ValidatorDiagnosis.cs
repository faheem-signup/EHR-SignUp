using Business.Handlers.Diagnosises.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorDiagnosis : AbstractValidator<CreateDiagnosisCommand>
    {
        public ValidatorDiagnosis()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.ICDCategoryId).NotEqual(0).WithMessage("Please select ICDCategory");
            RuleFor(p => p.ICDCategoryId).NotNull().WithMessage("Please select ICDCategory");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Please Enter Code!");
            RuleFor(p => p.Code).MaximumLength(50).WithMessage("Please Enter Code Less Than 50 Characters!");
            RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Please Enter Short Description!");
            RuleFor(p => p.ShortDescription).MaximumLength(50).WithMessage("Please Enter Short Description Less Than 50 Characters!");
            RuleFor(p => p.Description).MaximumLength(50).WithMessage("Please Enter Description Less Than 50 Characters!");
        }
    }

    public class ValidatorUpdateDiagnosis : AbstractValidator<UpdateDiagnosisCommand>
    {
        public ValidatorUpdateDiagnosis()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.DiagnosisId).NotEqual(0).WithMessage("Please select DiagnosisId");
            RuleFor(p => p.DiagnosisId).NotNull().WithMessage("Please select DiagnosisId");
            RuleFor(p => p.ICDCategoryId).NotEqual(0).WithMessage("Please select ICDCategory");
            RuleFor(p => p.ICDCategoryId).NotNull().WithMessage("Please select ICDCategory");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Please Enter Code!");
            RuleFor(p => p.Code).MaximumLength(50).WithMessage("Please Enter Code Less Than 50 Characters!");
            RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Please Enter Short Description!");
            RuleFor(p => p.ShortDescription).MaximumLength(50).WithMessage("Please Enter Short Description Less Than 50 Characters!");
            RuleFor(p => p.Description).MaximumLength(50).WithMessage("Please Enter Description Less Than 50 Characters!");
        }
    }
}
