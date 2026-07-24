using ConferenceHallBooking.Application.DTOs.Requests;
using FluentValidation;

namespace ConferenceHallBooking.Application.Validators
{
    public class ConferenceHallBookingValidator
    {
        public class CreateConferenceHallRequestValidator : AbstractValidator<CreateConferenceHallRequest>
        {
            public CreateConferenceHallRequestValidator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100)
                    .WithMessage("Name cannot exceed 100 characters.");

                RuleFor(x => x.Capacity)
                    .GreaterThan(0)
                    .WithMessage("Capacity must be greater than 0.");

                RuleFor(x => x.BasePricePerHour)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Base price per hour must be greater than or equal to 0.");

                RuleForEach(x => x.Amenities)
                    .SetValidator(new AmenitiesRequestValidator());
            }

            public class AmenitiesRequestValidator : AbstractValidator<AmenitiesRequest>
            {
                public AmenitiesRequestValidator()
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Amenity name is required.")
                        .MaximumLength(100)
                        .WithMessage("Amenity name cannot exceed 100 characters.");

                    RuleFor(x => x.Price)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("Amenity price must be greater than or equal to 0.");
                }
            }
        }
    }
}
