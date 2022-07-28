using Business.Handlers.Providers.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorProvider : AbstractValidator<CreateProviderCommand>
    {
        public ValidatorProvider()
        {
            //RuleFor(p => p.Title).NotEmpty().WithMessage("Please Select Your Title!");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter Your FirstName!");
            //RuleFor(p => p.MI).NotEmpty().WithMessage("Please Enter Your MI");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter Your LastName");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select Your City!");
            //RuleFor(p => p.City).NotNull().WithMessage("Please Select Your City!");
            //RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter Your State!");
            //RuleFor(p => p.State).NotNull().WithMessage("Please Enter Your State!");
            //RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Your zip!");
            //RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Your zip!");
            //RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Enter Your Date Of Birth!");
            //RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Your physical Addr.!");
            RuleFor(p => p.Degree).NotEmpty().WithMessage("Please Enter Your Degree!");
            RuleFor(p => p.NPINumber).NotEmpty().WithMessage("Please Enter Your NPINumber!");
        }
    }

    public class ValidatorUpdateProvider : AbstractValidator<UpdateProviderCommand>
    {
        public ValidatorUpdateProvider()
        {
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Enter ProviderId!");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Enter ProviderId!");
            //RuleFor(p => p.Title).NotEmpty().WithMessage("Please Select Your Title!");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter Your FirstName!");
            //RuleFor(p => p.MI).NotEmpty().WithMessage("Please Enter Your MI");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter Your LastName");
            //RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select Your City!");
            //RuleFor(p => p.City).NotNull().WithMessage("Please Select Your City!");
            //RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter Your State!");
            //RuleFor(p => p.State).NotNull().WithMessage("Please Enter Your State!");
            //RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Your zip!");
            //RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Your zip!");
            //RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Enter Your Date Of Birth!");
            //RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Your physical Addr.!");
            RuleFor(p => p.Degree).NotEmpty().WithMessage("Please Enter Your Degree!");
            RuleFor(p => p.NPINumber).NotEmpty().WithMessage("Please Enter Your NPINumber!");
        }
    }
}
