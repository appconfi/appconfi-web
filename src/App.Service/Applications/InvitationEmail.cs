namespace App.Service.Applications
{
    public class InvitationEmail : Common.Email
    {
        private readonly string actionUrl;

        public InvitationEmail(string applicationName, string actionUrl, string senderName)
        {
            Subject = $"You was invited to collaborate with '{applicationName}'";
            this.actionUrl = actionUrl;
        }

        public override string GetBodyHtml()
        {
            return $"You've been invited to join Appconfi. Accept invitation: {actionUrl}";
        }
    }
}
