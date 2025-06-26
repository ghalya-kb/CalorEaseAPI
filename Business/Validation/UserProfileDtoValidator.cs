using Business.Localization;
using Entities.DTOs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Business.Validation
{
    public class UserProfileDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileDtoValidator(IMessageService localizer)
        {
            RuleFor(x => x.HeightCm)
            .GreaterThan(0)
            .WithMessage(x => localizer["GreaterThan", nameof(x.HeightCm), 0]);

            RuleFor(x => x.WeightKg)
                .GreaterThan(0)
                .WithMessage(x => localizer["GreaterThan", nameof(x.WeightKg), 0]);

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120)
                .WithMessage(x => localizer["InclusiveBetween", nameof(x.Age), 1, 120]);

            RuleFor(x => x.ActivityLevel)
                .NotEmpty()
                .WithMessage(x => localizer["NotEmpty", nameof(x.ActivityLevel)])
                .Must(val => new[] { "Low", "Medium", "High" }.Contains(val))
                .WithMessage(localizer["ActivityLevelMustBeValid"]);

            RuleFor(x => x.GoalType)
                .NotEmpty()
                .WithMessage(x => localizer["NotEmpty", nameof(x.GoalType)])
                .Must(val => new[] { "Lose", "Maintain", "Gain" }.Contains(val))
                .WithMessage(localizer["GoalTypeMustBeValid"]);
        }
    }
}
