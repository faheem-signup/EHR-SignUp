using Business.Handlers.PatientsDispensing.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientDispensing : AbstractValidator<CreatePatientDispensingCommand>
    {
        public ValidatorPatientDispensing()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.ProgramId).NotEqual(0).WithMessage("Please Select Program!");
            RuleFor(p => p.ProgramId).NotNull().WithMessage("Please Select Program!");
            RuleFor(p => p.DrugUsage).NotEmpty().WithMessage("Please Enter Drug Used!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Enter Start Date!");
            RuleFor(p => p.MainBottle).NotEmpty().WithMessage("Please Enter Main Bottle!");
            RuleFor(p => p.TotalDispensed).NotEmpty().WithMessage("Please Enter Total Dispensed!");
            RuleFor(p => p.TotalDispensed).NotNull().WithMessage("Please Enter Total Dispensed!");
            RuleFor(p => p.Remaining).NotEmpty().WithMessage("Please Enter Remaining!");
            RuleFor(p => p.Remaining).NotNull().WithMessage("Please Enter Remaining!");
            RuleFor(p => p.TotalQuantity).NotEmpty().WithMessage("Please Enter Total Quantity!");
            RuleFor(p => p.TotalQuantity).NotNull().WithMessage("Please Enter TotalQuantity!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Enter End Date!");
            RuleFor(p => p.DrugUsage).MaximumLength(50).WithMessage("Please Enter Drug Usage Less Than 50 Characters!");
            RuleFor(p => p.MainBottle).MaximumLength(50).WithMessage("Please Enter Main Bottle Less Than 50 Characters!");
        }
    }

    public class ValidatorUpdatePatientDispensing : AbstractValidator<UpdatePatientDispensingCommand>
    {
        public ValidatorUpdatePatientDispensing()
        {
            RuleFor(p => p.DispensingId).NotEqual(0).WithMessage("Please Select Dispensing!");
            RuleFor(p => p.DispensingId).NotNull().WithMessage("Please Select Dispensing!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.ProgramId).NotEqual(0).WithMessage("Please Select Program!");
            RuleFor(p => p.ProgramId).NotNull().WithMessage("Please Select Program!");
            RuleFor(p => p.DrugUsage).NotEmpty().WithMessage("Please Enter Drug Used!");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Please Enter Start Date!");
            RuleFor(p => p.MainBottle).NotEmpty().WithMessage("Please Enter Main Bottle!");
            RuleFor(p => p.TotalDispensed).NotEmpty().WithMessage("Please Enter Total Dispensed!");
            RuleFor(p => p.TotalDispensed).NotNull().WithMessage("Please Enter Total Dispensed!");
            RuleFor(p => p.Remaining).NotEmpty().WithMessage("Please Enter Remaining!");
            RuleFor(p => p.Remaining).NotNull().WithMessage("Please Enter Remaining!");
            RuleFor(p => p.TotalQuantity).NotEmpty().WithMessage("Please Enter Total Quantity!");
            RuleFor(p => p.TotalQuantity).NotNull().WithMessage("Please Enter TotalQuantity!");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Please Enter End Date!");
            RuleFor(p => p.DrugUsage).MaximumLength(50).WithMessage("Please Enter Drug Usage Less Than 50 Characters!");
            RuleFor(p => p.MainBottle).MaximumLength(50).WithMessage("Please Enter Main Bottle Less Than 50 Characters!");
        }
    }
}
