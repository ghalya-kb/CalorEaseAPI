using Entities.DTOs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Business.Validation
{
    public class UserProfileDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileDtoValidator(IStringLocalizer<UserProfileDtoValidator> localizer)
        {
            RuleFor(x => x.HeightCm)
                .GreaterThan(0)
                .WithMessage(localizer["GreaterThan", 0]);

            RuleFor(x => x.WeightKg)
                .GreaterThan(0)
                .WithMessage(localizer["GreaterThan", 0]);

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120)
                .WithMessage(localizer["InclusiveBetween", 1, 120]);

            RuleFor(x => x.ActivityLevel)
                .NotEmpty()
                .WithMessage(localizer["NotEmpty"])
                .Must(x => new[] { "Low", "Medium", "High" }.Contains(x))
                .WithMessage(localizer["ActivityLevelMustBeValid"]);

            RuleFor(x => x.GoalType)
                .NotEmpty()
                .WithMessage(localizer["NotEmpty"])
                .Must(x => new[] { "Lose", "Maintain", "Gain" }.Contains(x))
                .WithMessage(localizer["GoalTypeMustBeValid"]);
        }
    }
}
