using api.Domain.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;

namespace api.Domain.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string link, string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "vitya_skazetski@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<h1>Привет! перейдите по ссылке чтобы подтвердить email!</h1><a href='{link}'>Ссылка</a>",
            };
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync("vitya_skazetski@mail.ru", "4qjb4BfB952tK0G0j9Bf");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);   
        }
    }
}
