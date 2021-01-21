namespace App.Service.Email.SendGrid
{
    public class SendGridConfig
    {
        public string ApiKey { get; set; }

        public string ApiId { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }
    }
}
