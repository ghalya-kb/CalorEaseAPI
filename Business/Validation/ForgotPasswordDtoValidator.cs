using Business.Localization;
using Entities.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator(IMessageService messages)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => messages["NotEmpty", nameof(x.Email)])
                .EmailAddress()
                .WithMessage(messages["InvalidEmail"]);
        }
    }
}
