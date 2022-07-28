using Business.Handlers.PatientVital.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatientVitals : AbstractValidator<CreatePatientVitalsCommand>
    {
        public ValidatorPatientVitals()
        {
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select Provider!");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select Provider!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.VisitDate).NotEmpty().WithMessage("Please Select Visit Date!");
            RuleFor(p => p.Height).MaximumLength(10).WithMessage("Please Enter Height Less Than 10 Characters!");
            RuleFor(p => p.Weight).MaximumLength(10).WithMessage("Please Enter Weight Less Than 10 Characters!");
            RuleFor(p => p.BMI).MaximumLength(10).WithMessage("Please Enter BMI Less Than 10 Characters!");
            RuleFor(p => p.Waist).MaximumLength(10).WithMessage("Please Enter Waist Less Than 10 Characters!");
            RuleFor(p => p.SystolicBP).MaximumLength(10).WithMessage("Please Enter Sys tolic BP Less Than 10 Characters!");
            RuleFor(p => p.DiaSystolicBP).MaximumLength(10).WithMessage("Please Enter Dia Sys tolic BP Less Than 10 Characters!");
            RuleFor(p => p.HeartRate).MaximumLength(10).WithMessage("Please Enter Heart Rate Less Than 10 Characters!");
            RuleFor(p => p.RespiratoryRate).MaximumLength(10).WithMessage("Please Enter Respiratory Rate Less Than 10 Characters!");
            RuleFor(p => p.Temprature).MaximumLength(10).WithMessage("Please Enter Temprature Less Than 10 Characters!");
            RuleFor(p => p.PainScale).MaximumLength(10).WithMessage("Please Enter Pain Scale Less Than 10 Characters!");
            RuleFor(p => p.HeadCircumference).MaximumLength(10).WithMessage("Please Enter Head Circumference  Less Than 10 Characters!");
            RuleFor(p => p.Comment).MaximumLength(200).WithMessage("Please Enter Comment Less Than 200 Characters!");
        }
    }

    public class ValidatorUpdatePatientVitals : AbstractValidator<UpdatePatientVitalsCommand>
    {
        public ValidatorUpdatePatientVitals()
        {
            RuleFor(p => p.PatientVitalsId).NotEqual(0).WithMessage("Please Enter PatientVitalsId!");
            RuleFor(p => p.PatientVitalsId).NotNull().WithMessage("Please Enter PatientVitalsId!");
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select Provider!");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select Provider!");
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient!");
            RuleFor(p => p.VisitDate).NotEmpty().WithMessage("Please Select Visit Date!");
            RuleFor(p => p.Height).MaximumLength(10).WithMessage("Please Enter Height Less Than 10 Characters!");
            RuleFor(p => p.Weight).MaximumLength(10).WithMessage("Please Enter Weight Less Than 10 Characters!");
            RuleFor(p => p.BMI).MaximumLength(10).WithMessage("Please Enter BMI Less Than 10 Characters!");
            RuleFor(p => p.Waist).MaximumLength(10).WithMessage("Please Enter Waist Less Than 10 Characters!");
            RuleFor(p => p.SystolicBP).MaximumLength(10).WithMessage("Please Enter Sys tolic BP Less Than 10 Characters!");
            RuleFor(p => p.DiaSystolicBP).MaximumLength(10).WithMessage("Please Enter Dia Sys tolic BP Less Than 10 Characters!");
            RuleFor(p => p.HeartRate).MaximumLength(10).WithMessage("Please Enter Heart Rate Less Than 10 Characters!");
            RuleFor(p => p.RespiratoryRate).MaximumLength(10).WithMessage("Please Enter Respiratory Rate Less Than 10 Characters!");
            RuleFor(p => p.Temprature).MaximumLength(10).WithMessage("Please Enter Temprature Less Than 10 Characters!");
            RuleFor(p => p.PainScale).MaximumLength(10).WithMessage("Please Enter Pain Scale Less Than 10 Characters!");
            RuleFor(p => p.HeadCircumference).MaximumLength(10).WithMessage("Please Enter Head Circumference  Less Than 10 Characters!");
            RuleFor(p => p.Comment).MaximumLength(200).WithMessage("Please Enter Comment Less Than 200 Characters!");
        }
    }
}
