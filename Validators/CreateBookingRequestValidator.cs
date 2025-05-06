using CoworkingReservationSystem.Models.DTOs.Requests;
using FluentValidation;

namespace CoworkingReservationSystem.Validators
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("Room ID must be greater than 0");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required")
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time");
        }
    }
}