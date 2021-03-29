using App.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Targeting
{
    public interface IUserTargetingService
    {
        Task CreatePerPercent(Guid applicationId, Guid environmentId, Guid featureToggleId, int percent);
        Task CreatePerUser(Guid applicationId, Guid environmentId, Guid featureToggleId, string property, string userList, TargetOption option);
        Task Delete(Guid userTargetingId);
        Task<UserTargeting> GetTarget(Guid id);
        Task<IEnumerable<UserTargeting>> GetTargets(Guid environemntId);
        Task UpdatePerPercent(Guid id, int percent);
        Task UpdatePerUser(Guid id, string property, string userList, TargetOption option);
    }
}