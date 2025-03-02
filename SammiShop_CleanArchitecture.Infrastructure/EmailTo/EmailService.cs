using SammiShop_CleanArchitecture.Infrastructure.Constant;
using System.Net;
using System.Net.Mail;

namespace SammiShop_CleanArchitecture.Infrastructure.EmailTo
{
    public class EmailService : IEmailService, IDisposable
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly string _fromEmail;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
            _fromEmail = emailConfiguration.From;
        }

        public async Task<string> SendEmailAsync(EmailTo emailTo)
        {
            using (var smtpClient = new SmtpClient(_emailConfiguration.SmtpServer))
            {
                try
                {
                    smtpClient.Port = _emailConfiguration.Port;
                    smtpClient.Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Timeout = ConstantInfrastructure.TIME_OUT;
                    using (var message = new MailMessage())
                    {
                        var fromAddress = new MailAddress(_fromEmail, "Sammi Shop");
                        message.From = fromAddress;
                        message.To.Add(emailTo.Mail);
                        message.Subject = emailTo.Subject;
                        message.Body = emailTo.Content;
                        message.IsBodyHtml = true;

                        await smtpClient.SendMailAsync(message);

                        return ConstantInfrastructure.SEND_MAIL_SUCCESS;
                    }
                }
                catch (SmtpException ex)
                {
                    return ConstantInfrastructure.SEND_MAIL_FAIL;
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}