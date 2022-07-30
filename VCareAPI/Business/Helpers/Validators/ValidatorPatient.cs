using Business.Handlers.Patients.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorPatient : AbstractValidator<CreatePatientCommand>
    {
        public ValidatorPatient()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter First Name!");
            //RuleFor(p => p.MiddleName).NotEmpty().WithMessage("Please Enter Middle Name!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter Last Name!");
            //RuleFor(p => p.Suffix).NotEmpty().WithMessage("Please Enter Suffix!");

            RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Enter Date of Birth!");
            RuleFor(p => p.Gender).NotEmpty().WithMessage("Please Select Gender!");
            //RuleFor(p => p.SSN).NotEmpty().WithMessage("Please Enter SSN!");
            //RuleFor(p => p.Suffix).NotEmpty().WithMessage("Suffix Required");

            RuleFor(p => p.AddressLine1).NotEmpty().WithMessage("Please Enter Address Line 1!");

            RuleFor(p => p.City).NotEmpty().WithMessage("Please Select City!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select City!");
            RuleFor(p => p.State).NotEmpty().WithMessage("Please Select State!");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Select State!");
            RuleFor(p => p.ZIP).NotEmpty().WithMessage("Please Select Zip!");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Select ZIP!");

            //RuleFor(p => p.CellPhone).NotEmpty().WithMessage("Please Enter Cell Phone!");
            //RuleFor(p => p.HomePhone).NotEmpty().WithMessage("Please Enter Home Phone!");
            //RuleFor(p => p.StatusId).NotEqual(0).WithMessage("Please Select Status!");
            //RuleFor(p => p.StatusId).NotNull().WithMessage("Please Select Status!");
        }
    }

    public class ValidatorUpdatePatient : AbstractValidator<UpdatePatientCommand>
    {
        public ValidatorUpdatePatient()
        {
            RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Enter PatientId!");
            RuleFor(p => p.PatientId).NotNull().WithMessage("Please Enter PatientId!");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Please Enter First Name!");
            //RuleFor(p => p.MiddleName).NotEmpty().WithMessage("Please Enter Middle Name!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Please Enter Last Name!");
            //RuleFor(p => p.Suffix).NotEmpty().WithMessage("Please Enter Suffix!");

            RuleFor(p => p.DOB).NotEmpty().WithMessage("Please Enter Date of Birth!");
            RuleFor(p => p.Gender).NotEmpty().WithMessage("Please Select Gender!");
            //RuleFor(p => p.SSN).NotEmpty().WithMessage("Please Enter SSN!");
            //RuleFor(p => p.Suffix).NotEmpty().WithMessage("Suffix Required");

            RuleFor(p => p.AddressLine1).NotEmpty().WithMessage("Please Enter Address Line 1!");

            RuleFor(p => p.City).NotEmpty().WithMessage("Please Select City!");
            RuleFor(p => p.City).NotEqual(0).WithMessage("Please Select City!");
            RuleFor(p => p.State).NotEmpty().WithMessage("Please Select State!");
            RuleFor(p => p.State).NotEqual(0).WithMessage("Please Select State!");
            RuleFor(p => p.ZIP).NotEmpty().WithMessage("Please Select Zip!");
            RuleFor(p => p.ZIP).NotEqual(0).WithMessage("Please Select ZIP!");

            //RuleFor(p => p.CellPhone).NotEmpty().WithMessage("Please Enter Cell Phone!");
            //RuleFor(p => p.HomePhone).NotEmpty().WithMessage("Please Enter Home Phone!");
            //RuleFor(p => p.StatusId).NotEqual(0).WithMessage("Please Select Status!");
            //RuleFor(p => p.StatusId).NotNull().WithMessage("Please Select Status!");
        }
    }

    public class ValidatorCreatePatientInfoDetails : AbstractValidator<CreatePatientInfoDetailsCommand>
    {
        public ValidatorCreatePatientInfoDetails()
        {
            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.SubstanceAbuseStatus).NotEqual(0).WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.SubstanceAbuseStatus).NotNull().WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.Alcohol).NotEqual(0).WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.Alcohol).NotNull().WithMessage("Please Select Alcohol!");
            RuleFor(p => p.IllicitSubstances).NotEqual(0).WithMessage("Please Select Illicit Substances!");
            RuleFor(p => p.IllicitSubstances).NotNull().WithMessage("Please Select Illicit Substances!");
            
        }
    }

    public class ValidatorUpdatePatientAdditionalInfo : AbstractValidator<UpdatePatientAdditionalInfoCommand>
    {
        public ValidatorUpdatePatientAdditionalInfo()
        {
            RuleFor(p => p.LocationId).NotEqual(0).WithMessage("Please Select Location!");
            RuleFor(p => p.LocationId).NotNull().WithMessage("Please Select Location!");
            RuleFor(p => p.SubstanceAbuseStatus).NotEqual(0).WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.SubstanceAbuseStatus).NotNull().WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.Alcohol).NotEqual(0).WithMessage("Please Select Substance Abuse Status!");
            RuleFor(p => p.Alcohol).NotNull().WithMessage("Please Select Alcohol!");
            RuleFor(p => p.IllicitSubstances).NotEqual(0).WithMessage("Please Select Illicit Substances!");
            RuleFor(p => p.IllicitSubstances).NotNull().WithMessage("Please Select Illicit Substances!");
        }
    }
}
