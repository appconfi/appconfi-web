using App.Domain;
using App.SharedKernel.Specifications;
using System;

namespace App.Service.Applications
{
    public static class ApplicationSpecifications
    {
        public static ISpecification<UserApplication> UserApplicationWithPermissions(Guid userId, ApplicationPermissions permission)
        {
            return new DirectSpecification<Domain.UserApplication>(x => x.UserId == userId && x.Permission >= permission);
        }

        public static ISpecification<Domain.UserApplication> UserApplicationById(Guid applicationId)
        {
            return new DirectSpecification<Domain.UserApplication>(x => x.ApplicationId == applicationId);
        }
    }
}
