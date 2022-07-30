using Business.Handlers.Contacts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorContact : AbstractValidator<CreateContactCommand>
    {
        public ValidatorContact()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please Enter Name!");
            RuleFor(p => p.Name).MaximumLength(50).WithMessage("Please Enter Name Less Than 50 Characters!");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Please Enter Phone Number!");
            RuleFor(p => p.Phone).MaximumLength(20).WithMessage("Please Enter Phone Number Less Than 20 Characters!");
            //RuleFor(p => p.Email).MaximumLength(50).WithMessage("Please Enter Email Address Less Than 50 Characters!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email Address");
            RuleFor(p => p.Fax).NotEmpty().WithMessage("Please Enter Fax!");
            RuleFor(p => p.Fax).MaximumLength(50).WithMessage("Please Enter Fax Less Than 50 Characters!");
            //RuleFor(p => p.Address).MaximumLength(50).WithMessage("Please Enter Address Less Than 50 Characters!");
            //RuleFor(p => p.ContactTypeId).NotEqual(0).WithMessage("Please Select Type!");
            //RuleFor(p => p.ContactTypeId).NotNull().WithMessage("Please Select Type!");
        }
    }

    public class ValidatorUpdateContact : AbstractValidator<UpdateContactCommand>
    {
        public ValidatorUpdateContact()
        {
            RuleFor(p => p.ContactId).NotEqual(0).WithMessage("Please Enter ContactId!");
            RuleFor(p => p.ContactId).NotEqual(0).WithMessage("Please Enter ContactId!");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please Enter Name!");
            RuleFor(p => p.Name).MaximumLength(50).WithMessage("Please Enter Name Less Than 50 Characters!");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Please Enter Phone Number!");
            RuleFor(p => p.Phone).MaximumLength(20).WithMessage("Please Enter Phone Number Less Than 20 Characters!");
            //RuleFor(p => p.Email).MaximumLength(50).WithMessage("Please Enter Email Address Less Than 50 Characters!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Enter Valid Email Address");
            RuleFor(p => p.Fax).NotEmpty().WithMessage("Please Enter Fax!");
            RuleFor(p => p.Fax).MaximumLength(50).WithMessage("Please Enter Fax Less Than 50 Characters!");
            //RuleFor(p => p.Address).MaximumLength(50).WithMessage("Please Enter Address Less Than 50 Characters!");
            //RuleFor(p => p.ContactTypeId).NotEqual(0).WithMessage("Please Select Type!");
            //RuleFor(p => p.ContactTypeId).NotNull().WithMessage("Please Select Type!");
        }
    }
}
