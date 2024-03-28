using CrudOperation.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace CrudOperation.Utility
{
    public class EmailServices
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailServices(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                    client.EnableSsl = true;

                    var message = new MailMessage(_smtpSettings.UserName, to, subject, body);
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
