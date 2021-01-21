using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class HasEnvironmentPermissionPolicy : IHasEnvironmentPermissionPolicy
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;

        public HasEnvironmentPermissionPolicy(IUnitOfWork unitOfWork, IHasApplicationPermissionPolicy hasApplicationPermission)
        {
            this.unitOfWork = unitOfWork;
            this.hasApplicationPermission = hasApplicationPermission;
        }

        public async Task<bool> ToRead(Guid userId, Guid environmentId)
        {
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();

            var spec = new DirectSpecification<Domain.ApplicationEnvironment>(x => x.Id == environmentId);
            var environment = await environments.FirstOrDefaultAsync(spec);

            return await hasApplicationPermission.ToRead(userId, environment.ApplicationId);
        }

        public async Task<bool> ToWrite(Guid userId, Guid environmentId)
        {
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();

            var spec = new DirectSpecification<Domain.ApplicationEnvironment>(x => x.Id == environmentId);
            var environment = await environments.FirstOrDefaultAsync(spec);

            return await hasApplicationPermission.ToRead(userId, environment.ApplicationId);
        }


    }
}
