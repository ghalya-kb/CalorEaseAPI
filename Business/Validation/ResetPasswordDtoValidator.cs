using Business.Localization;
using Entities.DTOs;
using FluentValidation;

namespace Business.Validation
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator(IMessageService messages)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(messages["InvalidEmail"]);
            RuleFor(x => x.Token).NotEmpty().WithMessage(x => messages["NotEmpty", nameof(x.Token)]);
            RuleFor(x => x.NewPassword)
                        .NotEmpty()
                        .WithMessage(messages["NotEmpty", nameof(ResetPasswordDto.NewPassword)])
                        .MinimumLength(6)
                        .WithMessage(messages["PasswordTooShort", 6])
                        .Matches(@"[0-9]+")
                        .WithMessage(messages["PasswordRequiresDigit"])
                        .Matches(@"[a-z]+")
                        .WithMessage(messages["PasswordRequiresLower"])
                        .Matches(@"[A-Z]+")
                        .WithMessage(messages["PasswordRequiresUpper"]);
        }
    }
}
