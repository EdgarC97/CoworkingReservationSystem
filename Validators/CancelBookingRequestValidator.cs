using CoworkingReservationSystem.Models.DTOs.Requests;
using FluentValidation;

namespace CoworkingReservationSystem.Validators
{
    public class CancelBookingRequestValidator : AbstractValidator<CancelBookingRequest>
    {
        public CancelBookingRequestValidator()
        {
            RuleFor(x => x.CancellationReason)
                .MaximumLength(500).WithMessage("Cancellation reason cannot exceed 500 characters");
        }
    }
}