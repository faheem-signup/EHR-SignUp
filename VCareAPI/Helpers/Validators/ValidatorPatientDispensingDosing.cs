using Business.Handlers.PatientsDispensing.Commands;
using Business.Handlers.PatientsDispensingDosing.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientDispensingDosing : AbstractValidator<CreatePatientDispensingDosingCommand>
    {
        public ValidatorPatientDispensingDosing()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.ProgramId).NotEqual(0).WithMessage("Please Select Program!");
            RuleFor(p => p.ProgramId).NotNull().WithMessage("Please Select Program!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Enter Start Date!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Enter End Date!");
            RuleFor(p => p.TherapistId).NotEqual(0).WithMessage("Please Select Therapist!");
            RuleFor(p => p.TherapistId).NotNull().WithMessage("Please Select Therapist!");
            RuleFor(p => p.LastDose).NotEmpty().WithMessage("Please Enter Last Dose!");
            RuleFor(p => p.TakeHome).NotEmpty().WithMessage("Please Enter Take Home!");
            RuleFor(p => p.MedicatedThru).NotEmpty().WithMessage("Please Enter Medicated Thru!");
            RuleFor(p => p.MedThruDose).NotEmpty().WithMessage("Please Enter Med.Thru Dose!");
        }
    }

    public class ValidatorUpdatePatientDispensingDosing : AbstractValidator<UpdatePatientDispensingDosingCommand>
    {
        public ValidatorUpdatePatientDispensingDosing()
        {
            RuleFor(p => p.DispensingDosingId).NotEqual(0).WithMessage("Please Enter DozingId!");
            RuleFor(p => p.DispensingDosingId).NotNull().WithMessage("Please Enter DozingId!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.ProgramId).NotEqual(0).WithMessage("Please Select Program!");
            RuleFor(p => p.ProgramId).NotNull().WithMessage("Please Select Program!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Enter Start Date!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Enter End Date!");
            RuleFor(p => p.TherapistId).NotEqual(0).WithMessage("Please Select Therapist!");
            RuleFor(p => p.TherapistId).NotNull().WithMessage("Please Select Therapist!");
            RuleFor(p => p.LastDose).NotEmpty().WithMessage("Please Enter Last Dose!");
            RuleFor(p => p.TakeHome).NotEmpty().WithMessage("Please Enter Take Home!");
            RuleFor(p => p.MedicatedThru).NotEmpty().WithMessage("Please Enter Medicated Thru!");
            RuleFor(p => p.MedThruDose).NotEmpty().WithMessage("Please Enter Med.Thru Dose!");
        }
    }
}
