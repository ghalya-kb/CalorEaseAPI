using Business.Localization;
using Entities.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class GetMealsByTypeRequestDtoValidator : AbstractValidator<GetMealsByTypeRequestDto>
    {
        public GetMealsByTypeRequestDtoValidator(IMessageService messages)
        {
            RuleFor(x => x.MealType)
                .NotEmpty().WithMessage(messages["NotEmpty", "MealType"])
                .Must(x => new[] { "Breakfast", "Lunch", "Dinner" }.Contains(x))
                .WithMessage(messages["InvalidMealType"]);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(messages["NotEmpty", "Date"]);
        }
    }
}
