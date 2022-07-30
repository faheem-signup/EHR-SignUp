using Business.Handlers.Locations.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorLocation : AbstractValidator<CreateLocationCommand>
    {
        public ValidatorLocation()
        {
            RuleFor(p => p.LocationName).NotEmpty().WithMessage("Please Enter Location Name!");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Location Address!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Enter City !");
            RuleFor(p => p.City).NotNull().WithMessage("Please Enter City !");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter State !");
            RuleFor(p => p.State).NotNull().WithMessage("Please Enter State !");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Zip !");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Zip !");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Please Enter Phone!");
            //RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email!");
        }
    }

    public class ValidatorUpdateLocation : AbstractValidator<UpdateLocationCommand>
    {
        public ValidatorUpdateLocation()
        {
            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please select LocationId");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please select LocationId");
            RuleFor(p => p.LocationName).NotEmpty().WithMessage("Please Enter Location Name!");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Please Enter Location Address!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Enter City !");
            RuleFor(p => p.City).NotNull().WithMessage("Please Enter City !");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Enter State !");
            RuleFor(p => p.State).NotNull().WithMessage("Please Enter State !");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Enter Zip !");
            RuleFor(p => p.ZIP).NotNull().WithMessage("Please Enter Zip !");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Please Enter Phone!");
            //RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email!");
        }
    }
}
