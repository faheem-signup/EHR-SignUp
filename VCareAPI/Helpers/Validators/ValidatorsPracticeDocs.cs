using Business.Handlers.Practices.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
   public class ValidatorsPracticeDocs : AbstractValidator<CreatePracticeDocsCommand>
    {
        public ValidatorsPracticeDocs()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("Please Enter Document Description!");
            RuleFor(p => p.DocumentData).NotEmpty().WithMessage("Please Select a file");
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please Select PracticeId!");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please Select PracticeId");
        }
    }
}
