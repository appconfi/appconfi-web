using App.Domain;
using App.Service.Applications;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Targeting
{
    public class UserTargetingService : IUserTargetingService
    {
        private readonly IHasEnvironmentPermissionPolicy hasEnvironmentPermission;
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;

        public UserTargetingService(
            IHasEnvironmentPermissionPolicy hasEnvironmentPermission,
            IAuthService authService,
            IUnitOfWork unitOfWork)
        {
            this.hasEnvironmentPermission = hasEnvironmentPermission;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserTargeting>> GetTargets(Guid environemntId)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasEnvironmentPermission.ToRead(userId, environemntId));

            var userTargetings = unitOfWork.Repository<UserTargeting, Guid>();

            return await userTargetings.GetAsync(UserTargeting.ByEnvironment(environemntId), "FeatureToggle,TargetRule");
        }

        public async Task CreatePerPercent(Guid applicationId, Guid environmentId, Guid featureToggleId, int percent)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasEnvironmentPermission.ToWrite(userId, environmentId), "You don't have permissions to create");

            var userTargetings = unitOfWork.Repository<UserTargeting, Guid>();


            Guard.IsFalse(await userTargetings.AnyAsync(UserTargeting.ByEnvironment(environmentId).And(UserTargeting.ByToggle(featureToggleId))), "Already exists a rule for this environment and this feature toggle");

            var target = UserTargeting.PerPercent(environmentId, featureToggleId, percent);
            userTargetings.Insert(target);

            await unitOfWork.SaveAsync();
        }

        public async Task CreatePerUser(Guid applicationId, Guid environmentId, Guid featureToggleId, string property, string userList, TargetOption option)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasEnvironmentPermission.ToWrite(userId, environmentId), "You don't have permissions to create");


            var userTargetings = unitOfWork.Repository<UserTargeting, Guid>();

            Guard.IsFalse(await userTargetings.AnyAsync(UserTargeting.ByEnvironment(environmentId).And(UserTargeting.ByToggle(featureToggleId))), "Already exists a rule for this environment and this feature toggle");


            var target = UserTargeting.PerUser(environmentId, featureToggleId, option, property, userList);
            userTargetings.Insert(target);

            await unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid userTargetingId)
        {
            var userId = authService.CurrentUserId();
            var userTargetings = unitOfWork.Repository<UserTargeting, Guid>();

            var userTarget = await userTargetings.GetById(userTargetingId);

            Guard.IsTrue(await hasEnvironmentPermission.ToWrite(userId, userTarget.EnvironmentId), "You don't have permissions to delete");

            userTargetings.Delete(userTarget);

            await unitOfWork.SaveAsync();
        }
    }
}
