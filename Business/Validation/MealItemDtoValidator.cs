using Business.Localization;
using Entities.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class MealItemDtoValidator : AbstractValidator<MealItemDto>
    {
        public MealItemDtoValidator(IMessageService messages)
        {
            RuleFor(x => x.FoodName)
                .NotEmpty().WithMessage(messages["NotEmpty", nameof(MealItemDto.FoodName)]);

            RuleFor(x => x.AmountGr)
                .GreaterThan(0).WithMessage(messages["GreaterThan", nameof(MealItemDto.AmountGr), 0]);

            RuleFor(x => x.Calories)
                .GreaterThan(0).WithMessage(messages["GreaterThan", nameof(MealItemDto.Calories), 0]);
        }
    }
}
