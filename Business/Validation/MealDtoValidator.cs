using Business.Localization;
using Entities.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class MealDtoValidator : AbstractValidator<MealDto>
    {
        public MealDtoValidator(IMessageService messages)
        {
            RuleFor(x => x.MealType)
                .NotEmpty().WithMessage(messages["NotEmpty", nameof(MealDto.MealType)]);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(messages["NotEmpty", nameof(MealDto.Date)]);

            RuleForEach(x => x.MealItems).SetValidator(new MealItemDtoValidator(messages));
        }
    }
}
