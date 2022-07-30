using Business.Handlers.UserApps.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
  public class ValidatorUserApp : AbstractValidator<CreateUserAppCommand>
    {
        public ValidatorUserApp()
        {
            RuleFor(p => p.UserTypeId).NotEqual(0).WithMessage("Please select User Type");
            RuleFor(p => p.UserTypeId).NotNull().WithMessage("Please select User Type");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter FirstName!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter LastName!");
            //RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Select Date of Birth!");
            //RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Physical Address !");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please select City");
            //RuleFor(p => p.City).NotNull().WithMessage("Please select City");
            //RuleFor(p => p.State).NotEqual(0).WithMessage("Please select State");
            //RuleFor(p => p.State).NotNull().WithMessage("Please select State");
            RuleFor(p => p.PersonalEmail).NotEmpty().WithMessage("Please Enter Personal Email!").EmailAddress().WithMessage("Please Enter Valid Email!");
            RuleFor(p => p.RoleId).NotEqual(0).WithMessage("Please select Role");
            RuleFor(p => p.RoleId).NotNull().WithMessage("Please select Role");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Please Enter Password!");
            //RuleFor(p => p.UserToLocationAssignIds).Must((instance, value) =>
            //{
            //    if (instance.UserToLocationAssignIds.Length == 0)
            //    {
            //        return false;
            //    }

            //    return true;
            //}).WithMessage("Please Select Assigned Location(s)");
        }
    }

    public class ValidatorUpdateUserApp : AbstractValidator<UpdateUserAppCommand>
    {
        public ValidatorUpdateUserApp()
        {
            RuleFor(p => p.UserId).NotEqual(0).WithMessage("Please select UserId");
            RuleFor(p => p.UserId).NotNull().WithMessage("Please select UserId");
            RuleFor(p => p.UserTypeId).NotEqual(0).WithMessage("Please select User Type");
            RuleFor(p => p.UserTypeId).NotNull().WithMessage("Please select User Type");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter FirstName!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter LastName!");
            //RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Select Date of Birth!");
            //RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Physical Address !");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please select City");
            //RuleFor(p => p.City).NotNull().WithMessage("Please select City");
            //RuleFor(p => p.State).NotEqual(0).WithMessage("Please select State");
            //RuleFor(p => p.State).NotNull().WithMessage("Please select State");
            RuleFor(p => p.RoleId).NotEqual(0).WithMessage("Please select Role");
            RuleFor(p => p.RoleId).NotNull().WithMessage("Please select Role");
            RuleFor(p => p.PersonalEmail).NotEmpty().WithMessage("Please Enter Personal Email!").EmailAddress().WithMessage("Please Enter Valid Email!");
            //RuleFor(p => p.UserToLocationAssignIds).Must((instance, value) =>
            //{
            //    if (instance.UserToLocationAssignIds.Length > 0)
            //    {
            //        return false;
            //    }

            //    return true;
            //}).WithMessage("Please Select Assigned Location(s)");
        }
    }
}
