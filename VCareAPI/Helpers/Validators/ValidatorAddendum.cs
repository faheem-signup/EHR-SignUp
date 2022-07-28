using Business.Handlers.Addendums.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorAddendum : AbstractValidator<CreateAddendumCommand>
    {
        public ValidatorAddendum()
        {
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select ProviderId!");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select ProviderId!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
        }
    }
}
