using Business.Handlers.ICDToPractice.Commands;
using Business.Handlers.ProcedureGroupToPractice.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorProcedureGroupToPractice : AbstractValidator<CreateProcedureGroupToPracticesCommand>
    {
        public ValidatorProcedureGroupToPractice()
        {
            RuleFor(p => p.PracticeId).NotEqual(0).WithMessage("Please select PracticeId");
            RuleFor(p => p.PracticeId).NotNull().WithMessage("Please select PracticeId");
            RuleFor(p => p.ProcedureGroupToPracticeList.Count).NotEqual(0).WithMessage("Please Select At Least One Practice CPT Group");
        }
    }
}
