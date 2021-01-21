using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class HasApplicationPermissionPolicy : IHasApplicationPermissionPolicy
    {
        private readonly IUnitOfWork unitOfWork;

        public HasApplicationPermissionPolicy(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ToDelete(Guid userId, Guid applicationId)
        {
            var userApplications = unitOfWork.Repository<Domain.UserApplication, int>();

            var spec = ApplicationSpecifications.UserApplicationWithPermissions(userId, Domain.ApplicationPermission.Owner).And(ApplicationSpecifications.UserApplicationById(applicationId));
            var application = await userApplications.FirstOrDefaultAsync(spec);

            return application != null;
        }

        public async Task<bool> ToRead(Guid userId, Guid applicationId)
        {
            var userApplications = unitOfWork.Repository<Domain.UserApplication, int>();

            var spec = ApplicationSpecifications.UserApplicationWithPermissions(userId, Domain.ApplicationPermission.Read).And(ApplicationSpecifications.UserApplicationById(applicationId));
            var application = await userApplications.FirstOrDefaultAsync(spec);

            return application != null;
        }

        public async Task<bool> ToWrite(Guid userId, Guid applicationId)
        {
            var userApplications = unitOfWork.Repository<Domain.UserApplication, int>();

            var spec = ApplicationSpecifications.UserApplicationWithPermissions(userId, Domain.ApplicationPermission.Admin).And(ApplicationSpecifications.UserApplicationById(applicationId));
            var application = await userApplications.FirstOrDefaultAsync(spec);

            return application != null;
        }
    }
}
