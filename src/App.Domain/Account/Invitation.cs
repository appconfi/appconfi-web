using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain
{
    public class Invitation : Entity
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public Guid ApplicationId { get; set; }

        public Application Application { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationPermissions Permission { get; set; }

        public static Invitation NewInvitation(string email, Application application)
        {
            Guard.IsNotNull(application);
            Guard.IsValidEmail(email, "Invalid email for this invitation");

            return new Invitation
            {
                ApplicationId = application.Id,
                Application = application,
                CreatedAt = DateTime.UtcNow,
                Email = email,
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString().Replace("-", ""),
                Permission = ApplicationPermissions.Admin
            };
        }

        public static ISpecification<Invitation> WithEmail(string email)
        {
            return new DirectSpecification<Invitation>(x => x.Email == email);
        }

        public static ISpecification<Invitation> WithApplication(Guid applicationId)
        {
            return new DirectSpecification<Invitation>(x => x.ApplicationId == applicationId);
        }

        public static ISpecification<Invitation> WithId(Guid invitationId)
        {
            return new DirectSpecification<Invitation>(x => x.Id == invitationId);
        }
    }
}
