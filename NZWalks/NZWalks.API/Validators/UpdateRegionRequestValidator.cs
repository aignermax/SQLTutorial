using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Population).GreaterThan(0);
            RuleFor(x => x.Area).GreaterThanOrEqualTo(0);
        }
    }
}
