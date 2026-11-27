// Holds the email details the app needs to send emails.
namespace CadenceStore.Models
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public EmailSettings()
        {
            From = "";
            SmtpServer = "";
            Port = 587;
            Username = "";
            Password = "";
        }
    }
}