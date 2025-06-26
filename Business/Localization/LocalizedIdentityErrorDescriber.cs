using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
namespace Business.Localization
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer _localizer;

        public LocalizedIdentityErrorDescriber(IStringLocalizerFactory factory)
        {
            var assemblyName = typeof(LocalizedIdentityErrorDescriber).Assembly.GetName().Name;
            _localizer = factory.Create("IdentityMessages", assemblyName);
        }

        public override IdentityError DuplicateEmail(string email)
            => new() { Code = nameof(DuplicateEmail), Description = _localizer[nameof(DuplicateEmail)] };

        public override IdentityError DuplicateUserName(string userName)
            => new() { Code = nameof(DuplicateUserName), Description = _localizer[nameof(DuplicateUserName)] };

        public override IdentityError PasswordTooShort(int length)
            => new() { Code = nameof(PasswordTooShort), Description = _localizer[nameof(PasswordTooShort), length] };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _localizer[nameof(PasswordRequiresNonAlphanumeric)] };

        public override IdentityError PasswordRequiresDigit()
            => new() { Code = nameof(PasswordRequiresDigit), Description = _localizer[nameof(PasswordRequiresDigit)] };

        public override IdentityError PasswordRequiresLower()
            => new() { Code = nameof(PasswordRequiresLower), Description = _localizer[nameof(PasswordRequiresLower)] };

        public override IdentityError PasswordRequiresUpper()
            => new() { Code = nameof(PasswordRequiresUpper), Description = _localizer[nameof(PasswordRequiresUpper)] };


        public override IdentityError InvalidEmail(string email)
            => new() { Code = nameof(InvalidEmail), Description = _localizer[nameof(InvalidEmail)] };
        public override IdentityError InvalidUserName(string userName)
            => new() { Code = nameof(InvalidUserName), Description = _localizer[nameof(InvalidUserName)] };

        public override IdentityError UserAlreadyHasPassword()
            => new() { Code = nameof(UserAlreadyHasPassword), Description = _localizer[nameof(UserAlreadyHasPassword)] };

        public override IdentityError UserLockoutNotEnabled()
            => new() { Code = nameof(UserLockoutNotEnabled), Description = _localizer[nameof(UserLockoutNotEnabled)] };

        public override IdentityError ConcurrencyFailure()
            => new() { Code = nameof(ConcurrencyFailure), Description = _localizer[nameof(ConcurrencyFailure)] };

        public override IdentityError DefaultError()
            => new() { Code = nameof(DefaultError), Description = _localizer[nameof(DefaultError)] };

        public override IdentityError InvalidToken()
            => new() { Code = nameof(InvalidToken), Description = _localizer[nameof(InvalidToken)] };

        public override IdentityError LoginAlreadyAssociated()
            => new() { Code = nameof(LoginAlreadyAssociated), Description = _localizer[nameof(LoginAlreadyAssociated)] };

        public override IdentityError UserAlreadyInRole(string role)
            => new() { Code = nameof(UserAlreadyInRole), Description = _localizer[nameof(UserAlreadyInRole), role] };

        public override IdentityError UserNotInRole(string role)
            => new() { Code = nameof(UserNotInRole), Description = _localizer[nameof(UserNotInRole), role] };

        public override IdentityError InvalidRoleName(string role)
            => new() { Code = nameof(InvalidRoleName), Description = _localizer[nameof(InvalidRoleName), role] };

        public override IdentityError DuplicateRoleName(string role)
            => new() { Code = nameof(DuplicateRoleName), Description = _localizer[nameof(DuplicateRoleName), role] };

        public override IdentityError PasswordMismatch()
            => new() { Code = nameof(PasswordMismatch), Description = _localizer[nameof(PasswordMismatch)] };
    }
}
