using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace PokemonShop.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly SmtpTemplate _smtpTemplate;

        public EmailService(IOptionsSnapshot<AppSettings> appSettings)
        {
            _smtpSettings = appSettings.Value.SmtpSettings;
            _smtpTemplate = appSettings.Value.SmtpTemplate;
        }

        public async Task SendEmailAsync(string name, string email)
        {
            var emailMessage = GetMessage(name, email);

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.UseSsl);
                await client.AuthenticateAsync(_smtpSettings.FromEmail, _smtpSettings.Password);
                await client.SendAsync(emailMessage);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        private MimeMessage GetMessage(string name, string email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_smtpTemplate.Name, _smtpSettings.FromEmail));
            emailMessage.To.Add(new MailboxAddress(name, email));
            emailMessage.Subject = _smtpTemplate.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = _smtpTemplate.Message
            };

            return emailMessage;
        }
    }
}