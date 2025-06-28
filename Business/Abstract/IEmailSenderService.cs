using Core.Utilities.Result;

namespace Business.Abstract
{
    public interface IEmailSenderService
    {
        Task<IResult> SendEmailAsync(string to, string subject, string body);
        Task<IResult> SendPasswordResetCodeAsync(string to, string resetCode);
        Task<IResult> SendEmailVerificationCodeAsync(string to, string confirmationCode);
    }
}
