namespace App.Service.Accounts
{
    public abstract class AppEmail : Common.Email
    {
    }

    public class WelcomeEmail : AppEmail
    {
        private readonly string actionUrl;

        public WelcomeEmail(string name, string actionUrl)
        {
            Subject = $"Welcome to Appconfi, {name}!";
            this.actionUrl = actionUrl;
        }

        public override string GetBodyHtml()
        {
            return $"Please active the account here: {actionUrl}";
        }
    }

    public class ForgotPasswordEmail : Common.Email
    {
        private readonly string linkWithToken;

        public ForgotPasswordEmail(string linkWithToken)
        {
            Subject = "Reset your password";
            this.linkWithToken = linkWithToken;
        }


        public override string GetBodyHtml()
        {
            return $"Reset your password. Use this link: {linkWithToken}";
        }
    }
}
