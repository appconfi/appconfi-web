using App.Domain;
using App.SharedKernel.Repository;
using System;
using System.Threading.Tasks;

namespace App.Service.Accounts
{
    public interface IActivityService
    {
        Task<PagedResult<ActivityLog>> GetLogs(Guid applicationId, int page = 0, int pageSize = 100);
        void Log(string name, string description, Guid applicationId, Guid userId);
        Task LogAsync(string name, string description, Guid applicationId, Guid userId);
    }
}
