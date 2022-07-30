using Business.Handlers.Rooms.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Validators
{
    public class ValidatorRoom : AbstractValidator<CreateRoomCommand>
    {
        public ValidatorRoom()
        {
            RuleFor(p => p.AreaId).NotEqual(0).WithMessage("Please Select Area!");
            RuleFor(p => p.AreaId).NotNull().WithMessage("Please Select Area!");
            RuleFor(p => p.RoomNumber).NotEmpty().WithMessage("Please Enter Room No!");
            RuleFor(p => p.RoomNumber).MaximumLength(15).WithMessage("Please Enter Room No Less Than 15 Characters!");
            RuleFor(p => p.RoomName).NotEmpty().WithMessage("Please Enter Room Name!");
            RuleFor(p => p.RoomName).MaximumLength(25).WithMessage("Please Enter Room Name Less Than 25 Characters!");
            RuleFor(p => p.TimeFrom).NotEmpty().WithMessage("Please Select Time!");
            RuleFor(p => p.TimeTo).NotEmpty().WithMessage("Please Select Time!");
        }
    }

    public class ValidatorUpdateRoom : AbstractValidator<UpdateRoomCommand>
    {
        public ValidatorUpdateRoom()
        {
            RuleFor(p => p.RoomId).NotEqual(0).WithMessage("Please Enter RoomId!");
            RuleFor(p => p.RoomId).NotNull().WithMessage("Please Enter RoomId!");
            RuleFor(p => p.AreaId).NotEqual(0).WithMessage("Please Select Area!");
            RuleFor(p => p.AreaId).NotNull().WithMessage("Please Select Area!");
            RuleFor(p => p.RoomNumber).NotEmpty().WithMessage("Please Enter Room No!");
            RuleFor(p => p.RoomNumber).MaximumLength(15).WithMessage("Please Enter Room No Less Than 15 Characters!");
            RuleFor(p => p.RoomName).NotEmpty().WithMessage("Please Enter Room Name!");
            RuleFor(p => p.RoomName).MaximumLength(25).WithMessage("Please Enter Room Name Less Than 25 Characters!");
            RuleFor(p => p.TimeFrom).NotEmpty().WithMessage("Please Select Time!");
            RuleFor(p => p.TimeTo).NotEmpty().WithMessage("Please Select Time!");
        }
    }
}
