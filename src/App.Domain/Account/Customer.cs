using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using System;

namespace App.Domain
{
    public class Customer : Entity
    {
        public string ExternalCustomerId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public static Customer New(User user, string customerExternalId)
        {
            Guard.IsNotNullOrEmpty(customerExternalId);
            Guard.IsNotNull(user);

            return new Customer
            {
                User = user,
                UserId = user.Id,
                ExternalCustomerId = customerExternalId,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
