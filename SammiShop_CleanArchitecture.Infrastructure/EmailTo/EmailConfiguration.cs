namespace SammiShop_CleanArchitecture.Infrastructure.EmailTo
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; } = default(int);
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
