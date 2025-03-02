namespace SammiShop_CleanArchitecture.Infrastructure.EmailTo
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(EmailTo emailTo);
    }
}
