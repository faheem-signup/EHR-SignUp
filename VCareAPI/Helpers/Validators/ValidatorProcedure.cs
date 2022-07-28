using Business.Handlers.Procedure.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
   public class ValidatorProcedure : AbstractValidator<CreateProceduresCommand>
    {
        public ValidatorProcedure()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.ProcedureGroupId).NotEqual(0).WithMessage("Please select CPT Groups");
            RuleFor(p => p.ProcedureGroupId).NotNull().WithMessage("Please select CPT Groups");
            RuleFor(p => p.ProcedureSubGroupId).NotEqual(0).WithMessage("Please select CPT Sub Group");
            RuleFor(p => p.ProcedureSubGroupId).NotNull().WithMessage("Please select CPT Sub Group");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Please Enter Code!");
            RuleFor(p => p.Code).MaximumLength(50).WithMessage("Please Enter Code Less Than 50 Characters!");
            RuleFor(p => p.NDCNumber).MaximumLength(50).WithMessage("Please Enter NDC Number Less Than 50 Characters!");
            RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Please Enter ShortDescription!");
            RuleFor(p => p.ShortDescription).MaximumLength(200).WithMessage("Please Enter Short Description Less Than 200 Characters!");
            RuleFor(p => p.Description).MaximumLength(200).WithMessage("Please Enter Description Less Than 200 Characters!");
            RuleFor(p => p.DefaultCharges).NotEmpty().WithMessage("Please Enter Default Modifier!");
            RuleFor(p => p.Date).NotEmpty().WithMessage("Please Enter Date!");
            RuleFor(p => p.MedicareAllowance).NotEmpty().WithMessage("Please Enter MedicareAllowance!");
        }
    }

    public class ValidatorUpdateProcedure : AbstractValidator<UpdateProceduresCommand>
    {
        public ValidatorUpdateProcedure()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.ProcedureId).NotEqual(0).WithMessage("Please Enter ProcedureId");
            RuleFor(p => p.ProcedureId).NotNull().WithMessage("Please Enter ProcedureId");
            RuleFor(p => p.ProcedureGroupId).NotEqual(0).WithMessage("Please select CPT Groups");
            RuleFor(p => p.ProcedureGroupId).NotNull().WithMessage("Please select CPT Groups");
            RuleFor(p => p.ProcedureSubGroupId).NotEqual(0).WithMessage("Please select CPT Sub Group");
            RuleFor(p => p.ProcedureSubGroupId).NotNull().WithMessage("Please select CPT Sub Group");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Please Enter Code!");
            RuleFor(p => p.Code).MaximumLength(50).WithMessage("Please Enter Code Less Than 50 Characters!");
            RuleFor(p => p.NDCNumber).MaximumLength(50).WithMessage("Please Enter NDC Number Less Than 50 Characters!");
            RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Please Enter ShortDescription!");
            RuleFor(p => p.ShortDescription).MaximumLength(200).WithMessage("Please Enter Short Description Less Than 200 Characters!");
            RuleFor(p => p.Description).MaximumLength(200).WithMessage("Please Enter Description Less Than 200 Characters!");
            RuleFor(p => p.DefaultCharges).NotEmpty().WithMessage("Please Enter Default Modifier!");
            RuleFor(p => p.Date).NotEmpty().WithMessage("Please Enter Date!");
            RuleFor(p => p.MedicareAllowance).NotEmpty().WithMessage("Please Enter MedicareAllowance!");
        }
    }
}
