using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using My_Books.helpers;
using System.Net;

using MailKit.Net.Smtp;

namespace My_Books.Data.Services
{
    public class mailingService : IMailingService
    {
        private readonly MailSettings _mailSettings;

        public mailingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        
        public async Task SendMails(string mailTo, string Subject, string Body/*, IList<IFormFile> attchments*/)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject=Subject
            };
            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();
            //if (attchments != null)
            //{
            //    byte[] fileBytes;
            //    foreach(var file in attchments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using var ms = new MemoryStream();
            //            file.CopyTo(ms);
            //            fileBytes = ms.ToArray();

            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}

            builder.HtmlBody = Body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
        }
    }
}
