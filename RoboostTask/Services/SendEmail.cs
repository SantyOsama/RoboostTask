using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace RoboostTask.Services
{

    public class SendEmail
    {
        private EmailSetting emailSetting;
        public SendEmail()
        {
            emailSetting = new EmailSetting();
            SettingOfEmail();
        }
        public void SettingOfEmail()
        {
            emailSetting.Email = "santytask@outlook.com";
            emailSetting.DisplayName = "Inventory Management System";
            emailSetting.Password = "Santy@123";
            emailSetting.Host = "smtp-mail.outlook.com";
            emailSetting.Port = 587;
            emailSetting.Subject = "System Mail";
        }
        public async Task SendEmailAsync(string mailTo, string body)
        {
            emailSetting.Body = body;
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailSetting.Email),
                Subject = emailSetting.Subject
            };
            // email.HtmlBody = true;
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            builder.HtmlBody = emailSetting.Body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(emailSetting.DisplayName, emailSetting.Email));

            var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSetting.Email, emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }

    public class EmailSetting
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}