using Business.Handlers.Addendums.Commands;
using Business.Handlers.FormTemplates.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorUpsertClinicalTemplateData : AbstractValidator<UpsertClinicalTemplateDataCommand>
    {
        public ValidatorUpsertClinicalTemplateData()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.TemplateId).NotEqual(0).WithMessage("Please Select ProviderId!");
            RuleFor(p => p.TemplateId).NotNull().WithMessage("Please Select ProviderId!");
            RuleFor(p => p.AppointmentId).NotEqual(0).WithMessage("Please Select AppointmentId!");
            RuleFor(p => p.AppointmentId).NotNull().WithMessage("Please Select AppointmentId!");
            RuleFor(p => p.FormData).NotEmpty().WithMessage("Please Add Patient Notes!");
        }
    }
}
