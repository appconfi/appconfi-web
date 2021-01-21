using App.SharedKernel.Domain;

namespace App.Domain
{
    public class UserRegistered : DomainEvent
    {
        public UserRegistered(User user)
        {
            User = user;
        }

        public User User { get; private set; }

        public override void Flatten()
        {
            Args.Add("UserId", User.Id);
            Args.Add("AccountId", User.Account.Id);
        }
    }
}
