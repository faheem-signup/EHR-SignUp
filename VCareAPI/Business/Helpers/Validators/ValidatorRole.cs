using Business.Handlers.Role.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorRole : AbstractValidator<CreateRolesCommand>
    {
        public ValidatorRole()
        {
            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Please Enter Role Name!");
            RuleFor(p => p.RoleName).MaximumLength(25).WithMessage("Please Enter Role Name Less Than 25 Characters!");
        }
    }

    public class ValidatorUpdateRole : AbstractValidator<UpdateRolesCommand>
    {
        public ValidatorUpdateRole()
        {
            RuleFor(p => p.RoleId).NotEqual(0).WithMessage("Please Enter RoleId!");
            RuleFor(p => p.RoleId).NotNull().WithMessage("Please Enter RoleId!");
            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Please Enter Role Name!");
            RuleFor(p => p.RoleName).MaximumLength(25).WithMessage("Please Enter Role Name Less Than 25 Characters!");
        }
    }
}
