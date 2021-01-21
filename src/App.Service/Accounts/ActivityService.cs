using App.Domain;
using App.Service.Applications;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Accounts
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IAuthService authService;
        private IRepository<ActivityLog, Guid> logs;

        public ActivityService(
            IUnitOfWork unitOfWork,
            IHasApplicationPermissionPolicy hasApplicationPermission,
            IAuthService authService)
        {
            logs = unitOfWork.Repository<ActivityLog, Guid>();
            this.unitOfWork = unitOfWork;
            this.hasApplicationPermission = hasApplicationPermission;
            this.authService = authService;
        }
        public void Log(string name, string description, Guid applicationId, Guid userId)
        {
            logs.Insert(ActivityLog.SuccessLog(name, description, applicationId, userId));
        }

        public async Task LogAsync(string name, string description, Guid applicationId, Guid userId)
        {
            logs.Insert(ActivityLog.SuccessLog(name, description, applicationId, userId));
            await unitOfWork.SaveAsync();
        }

        public async Task<PagedResult<ActivityLog>> GetLogs(Guid applicationId, int page = 0, int pageSize = 100)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), "Invalid permissions for read activity logs");

            var count = await logs.CountAsync(ActivityLog.ByApplication(applicationId));

            var result = await logs.GetAsync(ActivityLog.ByApplication(applicationId), page, pageSize, x => x.OrderByDescending(y => y.TimeStamp), "InitiatedBy.Account");

            return new PagedResult<ActivityLog>(result, count, pageSize, page);
        }
    }
}
