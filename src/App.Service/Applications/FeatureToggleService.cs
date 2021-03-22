using App.Domain.ActivityLogs;
using App.Domain.FeatureToggles;
using App.Service.Accounts;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly IActivityService activityService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IHasEnvironmentPermissionPolicy hasEnvironmentPermission;
        private readonly IAuthService authService;

        public FeatureToggleService(
            IActivityService activityService,
            IUnitOfWork unitOfWork,
            IHasApplicationPermissionPolicy hasApplicationPermission,
            IHasEnvironmentPermissionPolicy hasEnvironmentPermission,
            IAuthService authService)
        {
            this.activityService = activityService;
            this.unitOfWork = unitOfWork;
            this.hasApplicationPermission = hasApplicationPermission;
            this.hasEnvironmentPermission = hasEnvironmentPermission;
            this.authService = authService;
        }

        public async Task NewToggle(Guid applicationId, string key, string description, bool enable)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, applicationId), "You don't have permissions to create feature toggle");

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            var application = await applications.FirstOrDefaultAsync(new DirectSpecification<Domain.Application>(x => x.Id == applicationId), "FeatureToggles,Environments.FeatureToggleValues");

            application.AddNewFeatureToggleForDefaultEnv(key, description, enable);

            activityService.Log(
                LogString.WithName("Feature toggle", "created"),
                LogString.WithDescription(new Dictionary<string, string> { { "Name", key }, { "Value", enable.ToString() } }), applicationId, userId);

            await unitOfWork.SaveAsync();

        }

        public async Task EditToggle(Guid environmentId, Guid featureId, bool enable)
        {
            var userId = authService.CurrentUserId();
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();
            var features = unitOfWork.Repository<FeatureToggle, Guid>();
            var featureToggle = await features.GetByIdAsync(featureId);

            var environment = await environments.GetById(environmentId, "Application.FeatureToggles,FeatureToggleValues");

            Guard.IsNotNull(featureToggle, "Invalid feature toggle");
            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, environment.ApplicationId), "You don't have permissions to edit feature toggles");

            environment.AddOrEditFeatureToggleValue(featureId, enable);
            activityService.Log(
                LogString.WithName("Feature toggle", "edited"),
                LogString.WithDescription(new Dictionary<string, string> { { "Name", featureToggle.Key }, { "Value", enable.ToString() }, { "Environment", environment.Name } }), environment.ApplicationId, userId);

            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FeatureToggle>> GetFeatureToggles(Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions for read settings fro this application");
            var featureToggles = unitOfWork.Repository<FeatureToggle, Guid>();

            var spec = new DirectSpecification<FeatureToggle>(x => x.ApplicationId == applicationId);
            var appFeatures = await featureToggles.GetAsync(spec);

            return appFeatures;
        }

        public async Task<FeatureToggle> GetFeatureToggle(Guid applicationId, Guid featureToggleId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions");
            var featureToggles = unitOfWork.Repository<FeatureToggle, Guid>();

            var spec = new DirectSpecification<FeatureToggle>(x => x.ApplicationId == applicationId && x.Id == featureToggleId);
            var featureToggle = await featureToggles.FirstOrDefaultAsync(spec);

            return featureToggle;
        }

        public async Task<IEnumerable<FeatureToggleValue>> GetValues(Guid applicationId, Guid featureToggleId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions");

            var values = unitOfWork.Repository<FeatureToggleValue, Guid>();

            var spec = new DirectSpecification<FeatureToggleValue>(x => x.Environment.ApplicationId == applicationId && x.FeatureToggleId == featureToggleId);
            var featureValues = await values.GetAsync(spec);

            return featureValues;
        }

        public async Task<IEnumerable<FeatureToggleValue>> GetValues(Guid environmentId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasEnvironmentPermission.ToRead(userId, environmentId), "Invalid permissions for read this environment");

            var values = unitOfWork.Repository<FeatureToggleValue, Guid>();

            var spec = new DirectSpecification<FeatureToggleValue>(x => x.EnvironmentId == environmentId);
            var appValues = await values.GetAsync(spec);

            return appValues;
        }

        public async Task<IEnumerable<ApplicationKeyFeatureDto>> GetKeyValues(Guid environmentId)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasEnvironmentPermission.ToRead(userId, environmentId), "Invalid permissions for read environments");

            var featureToggles = unitOfWork.Repository<FeatureToggle, Guid>();
            var values = unitOfWork.Repository<FeatureToggleValue, Guid>();
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();

            var environment = await environments.FirstOrDefaultAsync(new DirectSpecification<Domain.ApplicationEnvironment>(x => x.Id == environmentId));

            var appFeatures = await featureToggles.GetAsync(new DirectSpecification<FeatureToggle>(x => x.ApplicationId == environment.ApplicationId));
            var appValues = await values.GetAsync(new DirectSpecification<FeatureToggleValue>(x => x.EnvironmentId == environmentId));

            var keyvalues = new List<ApplicationKeyFeatureDto>(appFeatures.Count());

            appFeatures.ToList().ForEach(s =>
            {
                var v = appValues.FirstOrDefault(x => x.FeatureToggleId == s.Id);
                keyvalues.Add(new ApplicationKeyFeatureDto
                {
                    Key = s.Key,
                    IsEnabled = v != null ? v.IsEnabled : false,
                    CreatedAt = v != null ? v.CreatedAt : s.CreatedAt,
                    LastModifiedAt = v?.LastModifiedAt,
                    SettingId = s.Id,
                });
            });

            return keyvalues.OrderBy(x => x.Key);

        }

        public async Task<IEnumerable<ApplicationEnvKeyFeatureDto>> GetKeyValues(Guid applicationId, Guid featureToggleId)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions");

            var featureToggles = unitOfWork.Repository<FeatureToggle, Guid>();
            var values = unitOfWork.Repository<FeatureToggleValue, Guid>();
            var environments = unitOfWork.Repository<Domain.ApplicationEnvironment, Guid>();

            var appEnvs = await environments.GetAsync(new DirectSpecification<Domain.ApplicationEnvironment>(x => x.ApplicationId == applicationId), "Application");

            var appFeatures = await featureToggles.GetAsync(new DirectSpecification<FeatureToggle>(x => x.ApplicationId == applicationId));
            var appValues = await values.GetAsync(new DirectSpecification<FeatureToggleValue>(x => x.FeatureToggleId == featureToggleId));

            var keyvalues = new List<ApplicationEnvKeyFeatureDto>(appFeatures.Count());

            appEnvs.ToList().ForEach(e =>
            {
                var v = appValues.FirstOrDefault(x => x.EnvironmentId == e.Id);
                keyvalues.Add(new ApplicationEnvKeyFeatureDto
                {
                    IsEnabled = v != null ? v.IsEnabled : false,
                    CreatedAt = v != null ? v.CreatedAt : e.Application.CreatedAt,
                    LastModifiedAt = v?.LastModifiedAt,
                    EnvironmentId = e.Id,
                    EnvironmentName = e.Name
                });
            });

            return keyvalues.OrderBy(x => x.EnvironmentName);
        }

        public async Task RemoveFeatureToggle(Guid settingId)
        {
            var userId = authService.CurrentUserId();

            var featureToggles = unitOfWork.Repository<FeatureToggle, Guid>();
            var featureToggle = await featureToggles.GetById(settingId);

            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, featureToggle.ApplicationId), "You don't have permissions to remove feature toggles");

            featureToggles.Delete(featureToggle);

            activityService.Log(
               LogString.WithName("Feature toggle", "deleted"),
               LogString.WithDescription(new Dictionary<string, string> { { "Name", featureToggle.Key } }), featureToggle.ApplicationId, userId);

            await unitOfWork.SaveAsync();
        }
    }
}
