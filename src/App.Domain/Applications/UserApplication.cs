using App.SharedKernel.Domain;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain
{
    public class UserApplication : Entity<int>, ICreatedAt
    {
        public DateTime CreatedAt { get; set; }

        public virtual Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual User User { get; set; }

        public Guid UserId { get; set; }

        public ApplicationPermission Permission { get; set; }

        public static UserApplication GrantPermission(User user, Application application, ApplicationPermission permission)
        {
            return new UserApplication
            {
                Application = application,
                ApplicationId = application.Id,
                CreatedAt = DateTime.UtcNow,
                Permission = permission,
                User = user,
                UserId = user.Id
            };
        }

        public static ISpecification<UserApplication> WithOwner()
        {
            return new DirectSpecification<UserApplication>(x => x.Permission == ApplicationPermission.Owner);
        }

        public static ISpecification<UserApplication> WithApplication(Guid applicationId)
        {
            return new DirectSpecification<UserApplication>(x => x.ApplicationId == applicationId);
        }

        public static ISpecification<UserApplication> WithUser(Guid userId)
        {
            return new DirectSpecification<UserApplication>(x => x.UserId == userId);
        }
    }

    public enum ApplicationPermission
    {
        Owner = 3,
        Admin = 2,
        Read = 1,
        None = 0
    }
}
