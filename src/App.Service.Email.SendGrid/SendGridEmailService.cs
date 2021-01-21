using App.Service.Common;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Service.Email.SendGrid
{
    public partial class SendGridEmailService : IEmailService
    {

        readonly SendGridConfig config;
        readonly SendGridClient client;

        public SendGridEmailService(IOptions<SendGridConfig> options)
        {
            config = options.Value;
            client = new SendGridClient(config.ApiKey);
        }

        public async Task<bool> SendEmailAsync(Common.Email email)
        {
            try
            {
                var from = new EmailAddress(config.From, config.FromName);
                var to = new EmailAddress(email.To);

                var htmlContent = email.GetBodyHtml();
                var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
                var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
