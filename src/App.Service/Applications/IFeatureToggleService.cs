using App.Domain.FeatureToggles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IFeatureToggleService
    {
        Task EditToggle(Guid environmentId, Guid settingId, bool enable);
        Task<FeatureToggle> GetFeatureToggle(Guid applicationId, Guid featureToggleId);
        Task<IEnumerable<FeatureToggle>> GetFeatureToggles(Guid applicationId);
        Task<IEnumerable<ApplicationKeyFeatureDto>> GetKeyValues(Guid environmentId);
        Task<IEnumerable<ApplicationEnvKeyFeatureDto>> GetKeyValues(Guid applicationId, Guid featureToggleId);
        Task<IEnumerable<FeatureToggleValue>> GetValues(Guid environmentId);
        Task<IEnumerable<FeatureToggleValue>> GetValues(Guid applicationId, Guid featureToggleId);
        Task NewToggle(Guid applicationId, string key, bool enable);
        Task RemoveFeatureToggle(Guid toggleId);
    }
}