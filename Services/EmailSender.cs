//USING STATEMENTS TO ENSURE WE HAVE ALL THE TOOLS WE NEED TO SUCCESFULLY EXECUTE THE EMAIL SENDER CLASS

//EMAIL TOOLS
using System.Net;
using System.Net.Mail;

// CONNECTING TO EMAIL SETTINGS MODEL
using CadenceStore.Models;

// ALLOWS FOR USE OF IEmailSender interface (Can't send the confirmation email without this).
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.Extensions.Options;

namespace CadenceStore.Services
{
    //ACTUALLY NOT INHERITANCE HERE, EMAIL SENDER CLASS IMPLEMENTING IEMAILSENDER INTERFACE (BASICALLY FOLLOWING THE REQUIREMENTS OF THE INTERFACE)
    public class EmailSender : IEmailSender
    {
        // SETTINGS STORES AN INSTANCE OF THE EMAILSETTINGS CLASS WITH THE VALUES NEEDED TO SEND EMAILS.
        private readonly EmailSettings _settings;

        //EMAILSENDER CONSTRUCTOR 
        //parameter data type is <emailsettings>, will store reference to the configured email settings class because of IOPTIONS and program cs configuration
        public EmailSender(IOptions<EmailSettings> emailSettingsOptions)
        {
            // emailSettingsOptions.Value here is the actual email settings object that will be stored in settings and used in the SendEmailAsync mehtod
            //which is a requirement found in the IEmailSender interface
            _settings = emailSettingsOptions.Value;
        }

        // Method required by IEmailSender interface to send emails (in this case the confirmation email)
        public async Task SendEmailAsync(
            string email,       // The recipient's email address.
            string subject,     // The email's subject line.
            string htmlMessage) // The email content; it can include a clickable link.
        {

            //ALL THE CLASSES INSTANTIATED FROM THIS POINT ARE A PART OF THE SYSTEM.NET.MAIL NAMESPACE 

            //Instantiates an instance of MailMessage class which is native to ASP.Net framework "using" cleans it up when this method finishes.
            using MailMessage messageObject = new MailMessage();

            //ASSIGNING VALUES TO THE MEMBER VARIABLES 
            // Sets what the person receiving the email will see as the from email. Instantiating with the from setting from EmailSettings object
            messageObject.From = new MailAddress(_settings.From);

            // Sets who receives the email.
            messageObject.To.Add(email);

            // Adds the subject and messageObject content.
            messageObject.Subject = subject;
            messageObject.Body = htmlMessage;

            // Tells the email system the body contains HTML formatting (the confirmation link).
            messageObject.IsBodyHtml = true;

            // Connects to the mail server and port stored in EmailSettings.
            using var smtpClient = new SmtpClient(
                _settings.SmtpServer,
                _settings.Port)
            {
                // Requests a secure connection to the mail server.
                EnableSsl = true,

                // Uses Gmail username and app password to sign in. Gets these credentials from the EmailSettingClass
                Credentials = new NetworkCredential(
                    _settings.Username,
                    _settings.Password)
            };

            // Sends the messageObject and waits for the sending attempt to finish.
            await smtpClient.SendMailAsync(messageObject);
        }
    }
}