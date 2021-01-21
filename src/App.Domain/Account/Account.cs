using App.SharedKernel;
using App.SharedKernel.Domain;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain
{
    public class Account : Entity, ICreatedAt
    {
        public string Email { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? VerifiedAt { get; set; }

        internal static Account New(string email, string password)
        {
            var salt = RandomSalt();

            return new Account
            {
                Id = Guid.NewGuid(),
                Email = email,
                Salt = salt,
                PasswordHash = EncodePassword(password, salt),
                CreatedAt = DateTime.UtcNow
            };
        }

        public void ResetPassword(string password)
        {
            Salt = RandomSalt();
            PasswordHash = EncodePassword(password, Salt);
        }

        static string RandomSalt()
        {
            return Guid.NewGuid()
                           .ToString()
                           .Replace("-", String.Empty)
                           .Substring(0, 8);
        }

        public bool IsValidPassword(string password)
        {
            return EncodePassword(password, Salt).Equals(PasswordHash);
        }

        public bool IsVerified { get { return VerifiedAt.HasValue; } }

        static string EncodePassword(string password, string salt)
        {
            return Security.Hash($"{salt}:{password}");
        }

        public void Verify()
        {
            VerifiedAt = DateTime.UtcNow;
        }

        public static ISpecification<Account> ByEmail(string email)
        {
            return new DirectSpecification<Account>(x => x.Email == email);
        }

    }
}
