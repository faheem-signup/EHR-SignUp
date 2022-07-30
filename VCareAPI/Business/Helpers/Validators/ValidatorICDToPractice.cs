using Business.Handlers.ICDToPractice.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorICDToPractice : AbstractValidator<CreateICDToPracticesCommand>
    {
        public ValidatorICDToPractice()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.ICDToPracticesIds.Length).NotEqual(0).WithMessage("Please Select At Least One Practice ICD Group");
        }
    }
}
