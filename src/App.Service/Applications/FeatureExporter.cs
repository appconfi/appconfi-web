using App.Domain;
using App.Domain.FeatureToggles;
using App.Service.Applications.Policies;
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
    public partial class FeatureExporter : IFeatureExporter
    {
        private readonly IAuthService authService;
        private readonly IHasValidAccessKeyPolicy hasValidAccessKey;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermissionPolicy;
        private readonly IUnitOfWork unitOfWork;

        public FeatureExporter(
            IAuthService authService,
            IHasValidAccessKeyPolicy hasValidAccessKey,
            IHasApplicationPermissionPolicy hasApplicationPermissionPolicy,
            IUnitOfWork unitOfWork)
        {
            this.authService = authService;
            this.hasValidAccessKey = hasValidAccessKey;
            this.hasApplicationPermissionPolicy = hasApplicationPermissionPolicy;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, object>> GetFeatures(Guid applicationId, Guid environmentId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermissionPolicy.ToRead(userId, applicationId), "Access denied");
            var environments = unitOfWork.Repository<ApplicationEnvironment, Guid>();

            var environment = await environments.FirstOrDefaultAsync(new DirectSpecification<ApplicationEnvironment>(x => x.ApplicationId == applicationId && x.Id == environmentId));
            return await GetFeatures(applicationId, environment);
        }
        public async Task<IDictionary<string, object>> GetFeatures(Guid applicationId, string environmentName, string privateKey)
        {
            var environments = unitOfWork.Repository<ApplicationEnvironment, Guid>();

            var environment = await environments.FirstOrDefaultAsync(new DirectSpecification<ApplicationEnvironment>(x => x.ApplicationId == applicationId && x.Name == environmentName));
            Guard.IsNotNull(environment, "Invalid environment name");
            Guard.IsTrue(await hasValidAccessKey.ToRead(environment.ApplicationId, privateKey), "Invalid access key");

            return await GetFeatures(applicationId, environment);
        }
        private async Task<IDictionary<string, object>> GetFeatures(Guid applicationId, ApplicationEnvironment environment)
        {
            Guard.IsNotNull(environment, "Invalid environment");

            var rules = await GetRules(environment.Id);
            var features = await GetFeaturesValues(environment.Id, applicationId);

            var result = new Dictionary<string, object>();
            features.ToList().ForEach(feature =>
            {
                result.Add(feature.Key, feature.Value);
            });

            rules.ToList().ForEach(rule =>
            {
                result[rule.FeatureToggle.Key] = RuleDto.NewRule((bool)result[rule.FeatureToggle.Key], rule.TargetRule);
            });

            return result;
        }

        private async Task<IEnumerable<UserTargeting>> GetRules(Guid environmentId)
        {
            var rules = unitOfWork.Repository<UserTargeting, Guid>();
            var envRules = await rules.GetAsync(UserTargeting.ByEnvironment(environmentId), "FeatureToggle,TargetRule");
            return envRules;
        }

        private async Task<IDictionary<string, bool>> GetFeaturesValues(Guid environmentId, Guid applicationId)
        {
            var result = new Dictionary<string, bool>();
            var features = unitOfWork.Repository<FeatureToggle, Guid>();
            var values = unitOfWork.Repository<FeatureToggleValue, Guid>();

            var appFeatures = await features.GetAsync(FeatureToggle.WithApplication(applicationId));
            appFeatures.ToList().ForEach(x => result.Add(x.Key, false));

            var appValues = await values.GetAsync(FeatureToggleValue.WithEnvironment(environmentId), "FeatureToggle");
            appValues.ToList().ForEach(x => result[x.FeatureToggle.Key] = x.IsEnabled);

            return result;
        }
    }
}
