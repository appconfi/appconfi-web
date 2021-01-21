using App.Domain;
using App.Domain.ActivityLogs;
using App.Service.Accounts;
using App.Service.Applications.Policies;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IHasValidAccessKeyPolicy hasValidAccessKey;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IAuthService authService;
        private readonly IActivityService activityService;

        public EnvironmentService(
            IHasValidAccessKeyPolicy hasValidAccessKey,
            IUnitOfWork unitOfWork,
            IHasApplicationPermissionPolicy hasApplicationPermission,
            IAuthService authService,
            IActivityService activityService)
        {
            this.hasValidAccessKey = hasValidAccessKey;
            this.unitOfWork = unitOfWork;
            this.hasApplicationPermission = hasApplicationPermission;
            this.authService = authService;
            this.activityService = activityService;
        }

        public async Task<IEnumerable<ApplicationEnvironment>> GetEnvironments(Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions for read environments");

            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();
            var envWithPermissionsSpec = new DirectSpecification<Domain.ApplicationEnvironment>(x => x.ApplicationId == applicationId);
            return await environments.GetAsync(envWithPermissionsSpec);
        }

        public async Task<Guid> CreateEnvironment(string name, Guid applicationId)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, applicationId), "Invalid permissions for create environments");

            var applications = unitOfWork.Repository<Application, Guid>();
            var application = await applications.FirstOrDefaultAsync(new DirectSpecification<Domain.Application>(x => x.Id == applicationId), "Environments");
            var env = application.AddEnvironment(name);

            activityService.Log(
               LogString.WithName("Environment", "created"),
               LogString.WithDescription(new Dictionary<string, string> { { "Name", name } }), applicationId, userId);

            await unitOfWork.SaveAsync();

            return env.Id;

        }

        public async Task<long> GetVersion(Guid applicationId, string environmentName, string privateKey)
        {
            Guard.IsTrue(await hasValidAccessKey.ToRead(applicationId, privateKey), "Invalid access key");

            var environments = unitOfWork.Repository<ApplicationEnvironment, Guid>();
            var spec = ApplicationEnvironment.WithApplication(applicationId).And(ApplicationEnvironment.WithName(environmentName));
            var environment = await environments.FirstOrDefaultAsync(spec);

            Guard.IsNotNull(environment, "Invalid environment name.");

            return environment.GetVersion();
        }



        public async Task DeleteEnvironment(Guid environmentId)
        {
            var userId = authService.CurrentUserId();
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();

            var environment = await environments.GetById(environmentId, "Application.Environments");

            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, environment.ApplicationId), "Invalid permissions for remove environments");

            var application = environment.Application;
            application.RemoveEnvironment(environment);

            activityService.Log(
              LogString.WithName("Environment", "deleted"),
              LogString.WithDescription(new Dictionary<string, string> { { "Name", environment.Name } }), environment.ApplicationId, userId);

            await unitOfWork.SaveAsync();
        }
    }
}
