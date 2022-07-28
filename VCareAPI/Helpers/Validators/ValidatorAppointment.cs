using Business.Handlers.Appointments.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorAppointment : AbstractValidator<CreateAppointmentCommand>
    {
        public ValidatorAppointment()
        {
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select Provider");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select Provider");
            RuleFor(p => p.AppointmentDate).NotEmpty().WithMessage("Please Enter AppointmentDate!");

            When(x => x.TimeFrom == null && x.TimeTo == null, () =>
            {
                RuleFor(p => p.TimeFrom).NotEmpty().WithMessage("Please Select Appointment Time!");
            });

            When(x => x.TimeFrom != null && x.TimeTo == null, () =>
            {
                RuleFor(p => p.TimeTo).NotEmpty().WithMessage("Please Select Appointment Time!");
            });

            When(x => x.TimeFrom == null && x.TimeTo != null, () =>
            {
                RuleFor(p => p.TimeFrom).NotEmpty().WithMessage("Please Select Appointment Time!");
            });

            //RuleFor(p => p.TimeFrom).NotNull().WithMessage("Please Select Time Slot!");
            //RuleFor(p => p.TimeTo).NotNull().WithMessage("Please Select Time Slot!");
            RuleFor(p => p.AppointmentDate).GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Appointment Date is not less than current date");
            RuleFor(p => p.TimeFrom).Must((instance, value) =>
            {
                if (instance.TimeFrom?.TimeOfDay > instance.TimeTo?.TimeOfDay)
                {
                    return false;
                }

                return true;
            }).WithMessage("TimeFrom is not less than TimeTo");

            //RuleFor(p => p.PatientId).Must((instance, value) =>
            //{
            //    if (!instance.AllowGroupPatient && (instance.PatientId == 0 || instance.PatientId == null))
            //    {
            //        return false;
            //    }

            //    return true;
            //}).WithMessage("Please Select Patient");

            //RuleFor(p => p.PatientId).Must((instance, value) =>
            //{
            //    if (instance.AllowGroupPatient && instance.PatientIds == null)
            //    {
            //        return false;
            //    }
            //    else if (instance.AllowGroupPatient && instance.PatientIds != null)
            //    {
            //        if (instance.PatientIds.Length == 0)
            //        {
            //            return false;
            //        }
            //    }

            //    return true;
            //}).WithMessage("Please Select Group Patients");

            When(x => !x.AllowGroupPatient && (x.PatientId == 0 || x.PatientId == null), () =>
            {
                RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient");
                RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient");
            });

            When(x => x.AllowGroupPatient && x.PatientIds == null, () =>
            {
                RuleFor(p => p.PatientIds).NotEmpty().WithMessage("Please Select Group Patients");
            });

            When(x => x.AllowGroupPatient && x.PatientIds != null, () =>
            {
                When(x => x.AllowGroupPatient && x.PatientIds.Length == 0, () =>
                {
                    RuleFor(p => p.PatientIds).NotEmpty().WithMessage("Please Select Group Patients");
                });
            });

            RuleFor(p => p.AppointmentStatus).NotEqual(0).WithMessage("Please Select Appointment Status");
            RuleFor(p => p.AppointmentStatus).NotNull().WithMessage("Please Select Appointment Status");
        }
    }

    public class ValidatorUpdateAppointment : AbstractValidator<UpdateAppointmentCommand>
    {
        public ValidatorUpdateAppointment()
        {

            //When(x => x.AllowGroupPatient == false && x.IsRecurringAppointment == false && x.IsFollowUpAppointment == false, () =>
            //{
            //    RuleFor(p => p.AllowGroupPatient).Equal(false).WithMessage("Please Select Group Patients");
            //});

            RuleFor(p => p.AppointmentId).NotEqual(0).WithMessage("Please Select AppointmentId");
            RuleFor(p => p.AppointmentId).NotNull().WithMessage("Please Select AppointmentId");
            RuleFor(p => p.ProviderId).NotEqual(0).WithMessage("Please Select Provider");
            RuleFor(p => p.ProviderId).NotNull().WithMessage("Please Select Provider");
            RuleFor(p => p.AppointmentDate).NotEmpty().WithMessage("Please Enter AppointmentDate!");
            //RuleFor(p => p.TimeFrom).NotEmpty().WithMessage("Please Enter TimeFrom");
            //RuleFor(p => p.TimeTo).NotEmpty().WithMessage("Please Enter TimeTo");
            RuleFor(p => p.AppointmentDate).GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Appointment Date is not less than current date");
            RuleFor(p => p.TimeFrom).Must((instance, value) =>
            {
                if (instance.TimeFrom?.TimeOfDay > instance.TimeTo?.TimeOfDay)
                {
                    return false;
                }

                return true;
            }).WithMessage("TimeFrom is not less than TimeTo");

            //RuleFor(p => p.PatientId).Must((instance, value) =>
            //{
            //    if (!instance.AllowGroupPatient && (instance.PatientId == 0 || instance.PatientId == null))
            //    {
            //        return false;
            //    }

            //    return true;
            //}).WithMessage("Please Select Patient");

            //RuleFor(p => p.PatientId).Must((instance, value) =>
            //{
            //    if (instance.AllowGroupPatient && instance.PatientIds == null)
            //    {
            //        return false;
            //    }
            //    else if (instance.AllowGroupPatient && instance.PatientIds != null)
            //    {
            //        if (instance.PatientIds.Length == 0)
            //        {
            //            return false;
            //        }
            //    }

            //    return true;
            //}).WithMessage("Please Select Group Patients");

            When(x => !x.AllowGroupPatient && (x.PatientId == 0 || x.PatientId == null), () =>
            {
                RuleFor(p => p.PatientId).NotEqual(0).WithMessage("Please Select Patient");
                RuleFor(p => p.PatientId).NotNull().WithMessage("Please Select Patient");
            });

            When(x => x.AllowGroupPatient && x.PatientIds == null, () =>
            {
                RuleFor(p => p.PatientIds).NotEmpty().WithMessage("Please Select Group Patients");
            });

            When(x => x.AllowGroupPatient && x.PatientIds != null, () =>
            {
                When(x => x.AllowGroupPatient && x.PatientIds.Length == 0, () =>
                {
                    RuleFor(p => p.PatientIds).NotEmpty().WithMessage("Please Select Group Patients");
                });
            });
            RuleFor(p => p.AppointmentStatus).NotEqual(0).WithMessage("Please Select Appointment Status");
            RuleFor(p => p.AppointmentStatus).NotNull().WithMessage("Please Select Appointment Status");

        }
    }
}
