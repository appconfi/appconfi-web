using App.Domain;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class AccessKeysService : IAccessKeysService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IAuthService authService;

        public AccessKeysService(
           IUnitOfWork unitOfWork,
           IHasApplicationPermissionPolicy hasApplicationPermission,
           IAuthService authService)
        {
            this.unitOfWork = unitOfWork;
            this.hasApplicationPermission = hasApplicationPermission;
            this.authService = authService;

        }
        public async Task<ApplicationAccessKey> GetKey(Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "You dont have permission to see this key");

            var applications = unitOfWork.Repository<Domain.Application, Guid>();

            var application = await applications.GetById(applicationId, "AccessKey");

            return application.AccessKey;
        }

        public async Task<ApplicationAccessKey> RegenerateKey(Guid applicationId)
        {
            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            var application = await applications.GetById(applicationId, "AccessKey");

            var key = application.AccessKey;
            key = key.Generate();

            await unitOfWork.SaveAsync();

            return key;
        }
    }
}
