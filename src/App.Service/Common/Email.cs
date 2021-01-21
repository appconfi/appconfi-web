namespace App.Service.Common
{
    public abstract class Email
    {
        public string Subject { get; set; }

        public string To { get; set; }

        public abstract string GetBodyHtml();
    }
}
