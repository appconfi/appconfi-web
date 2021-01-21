using App.Domain;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Threading.Tasks;

namespace App.Service.Applications.Policies
{
    public class HasValidAccessKeyPolicy : IHasValidAccessKeyPolicy
    {
        private readonly IUnitOfWork unitOfWork;

        public HasValidAccessKeyPolicy(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> ToRead(Guid applicationId, string key)
        {
            return await ValidKey(applicationId, key);
        }

        private async Task<bool> ValidKey(Guid applicationId, string key)
        {
            var keys = unitOfWork.Repository<ApplicationAccessKey, Guid>();
            var validKey = await keys.AnyAsync(new DirectSpecification<ApplicationAccessKey>(x => x.ApplicationId == applicationId && x.Secret == key));
            return validKey;
        }
    }
}
