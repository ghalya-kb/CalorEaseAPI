using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using MimeKit;
using MailKit.Net.Smtp;
using System.Resources;
using Business.Localization;
using System.Globalization;
using System.Reflection;

namespace Business.Concrete
{

    public class EmailSenderManager : IEmailSenderService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587; //TLS, Use 465 for SSL  
        private readonly string _smtpUser = "calorease.api@gmail.com"; // Your Gmail address  
        private readonly string _smtpPass = "jyzg uzet afgm kgni"; // App password or Gmail password  
        private readonly IMessageService _messages;

        public EmailSenderManager(IMessageService messages)
        {
            _messages = messages;
        }
        public async Task<IResult> SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ConstantStrings.AppName, _smtpUser));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            return await SendEmailAsync(message);
        }
        public async Task<IResult> SendPasswordResetCodeAsync(string to, string resetCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ConstantStrings.AppName, _smtpUser));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = _messages["PasswordResetCode"];

            string htmlTemplate = GetHtmlPasswordResetTemplate(resetCode);

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlTemplate,
                TextBody = $"{_messages["YourResetCodeIs"]}: {resetCode}"
            };

            message.Body = bodyBuilder.ToMessageBody();

            return await SendEmailAsync(message);

        }
        public async Task<IResult> SendEmailVerificationCodeAsync(string to, string confirmationCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ConstantStrings.AppName, _smtpUser));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = $"{ConstantStrings.AppName} {_messages["MailConfirmationCode"]}";

            string htmlTemplate = GetHtmlEmailVereficationTemplate(confirmationCode);

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlTemplate,
                TextBody = $"{_messages["YourResetCodeIs"]}: {confirmationCode}"
            };

            message.Body = bodyBuilder.ToMessageBody();

            return await SendEmailAsync(message);

        }
        private static string GetHtmlPasswordResetTemplate(string resetCode)
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "EmailTemplates", $"PasswordReset_{culture}.html");
            var htmlTemplate = File.ReadAllText(fileName);

            htmlTemplate = htmlTemplate.Replace("{{ResetCode}}", resetCode);

            return htmlTemplate;
        }
        private static string GetHtmlEmailVereficationTemplate(string resetCode)
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "EmailTemplates", $"EmailVerification_{culture}.html");
            var htmlTemplate = File.ReadAllText(fileName);

            htmlTemplate = htmlTemplate.Replace("{{ConfirmationCode}}", resetCode);

            return htmlTemplate;
        }
        private async Task<IResult> SendEmailAsync(MimeMessage message)
        {
            using var client = new SmtpClient();

            // Connect to Gmail SMTP server  
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

            // Authenticate with Gmail  
            await client.AuthenticateAsync(_smtpUser, _smtpPass);

            // Send the email  
            await client.SendAsync(message);

            await client.DisconnectAsync(true);

            return new SuccessResult(_messages["EmailSentSuccessfully"]);
        }


    }
}
