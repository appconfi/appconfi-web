using App.SharedKernel.Domain;
using System;

namespace App.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }

        public static User Register(string email, string password, string name, string lastName)
        {
            var account = Account.New(email, password);
            var user = new User
            {
                Account = account,
                FirstName = name,
                LastName = lastName,
                Id = Guid.NewGuid()
            };

            DomainEvents.Raise(new UserRegistered(user));

            return user;
        }
    }
}
