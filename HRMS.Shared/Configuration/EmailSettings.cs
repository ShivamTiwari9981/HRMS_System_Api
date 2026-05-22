namespace HRMS.Shared.Configuration
{
    public class EmailSettings
    {
        public string DisplayName { get; set; }

        public string FromEmail { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SmtpServer { get; set; }

        public int Port { get; set; }
    }
}
