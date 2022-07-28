using Business.Handlers.PatientCommunications.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
  public class ValidatorPatientCommunication : AbstractValidator<CreatePatientCommunicationCommand>
    {
        public ValidatorPatientCommunication()
        {
            RuleFor(p => p.CommunicationDate).NotEmpty().WithMessage("Please Select Date!");
            RuleFor(p => p.CommunicationTime).NotEmpty().WithMessage("Please Select Time!");
            RuleFor(p => p.CallDetailDescription).NotEmpty().WithMessage("Please Enter Call Detail!");
            //RuleFor(p => p.CommunicateBy).NotEqual(0).WithMessage("Please Select User!");
            //RuleFor(p => p.CommunicateBy).NotNull().WithMessage("Please Select User!");
        }
    }

    public class ValidatorUpdatePatientCommunication : AbstractValidator<UpdatePatientCommunicationCommand>
    {
        public ValidatorUpdatePatientCommunication()
        {
            RuleFor(p => p.CommunicationId).NotEqual(0).WithMessage("Please Select CommunicationId!");
            RuleFor(p => p.CommunicationId).NotNull().WithMessage("Please Select CommunicationId!");
            RuleFor(p => p.CommunicationDate).NotEmpty().WithMessage("Please Select Date!");
            RuleFor(p => p.CommunicationTime).NotEmpty().WithMessage("Please Select Time!");
            RuleFor(p => p.CallDetailDescription).NotEmpty().WithMessage("Please Enter Call Detail!");
            //RuleFor(p => p.CommunicateBy).NotEqual(0).WithMessage("Please Select User!");
            //RuleFor(p => p.CommunicateBy).NotNull().WithMessage("Please Select User!");
        }
    }
}
