using App.Domain.ActivityLogs;
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
    public class ApplicationService : IApplicationService
    {
        private readonly IActivityService activity;
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthService authService;

        public ApplicationService(
            IActivityService activity,
            IHasApplicationPermissionPolicy hasApplicationPermission,
            IUnitOfWork unitOfWork,
            IAuthService authService)
        {
            this.activity = activity;
            this.hasApplicationPermission = hasApplicationPermission;
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public async Task<Guid> CreateApplication(string name)
        {
            var user = await authService.CurrentUser();

            var application = Domain.Application.New(user, name);

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            applications.Insert(application);

            activity.Log(
              LogString.WithName("Application", "created"),
              LogString.WithDescription(new Dictionary<string, string> { { "Name", name } }), application.Id, user.Id);

            await unitOfWork.SaveAsync();
            return application.Id;

        }

        public async Task<Domain.Application> GetApplication(Guid id)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, id), "Does not have permission for read the application");

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            return await applications.FirstOrDefaultAsync(new DirectSpecification<Domain.Application>(x => x.Id == id));
        }

        public async Task DeleteApplication(Guid id, string applicationName)
        {
            var userId = authService.CurrentUserId();

            Guard.IsTrue(await hasApplicationPermission.ToDelete(userId, id), "Does not have permission for delete the application. Contact with the owner of this application");

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            var application = await applications.GetById(id);

            Guard.IsTrue(application.Name == applicationName, "The application name is not correct");

            applications.Delete(application);

            activity.Log(
            LogString.WithName("Application", "deleted"),
            LogString.WithDescription(new Dictionary<string, string> { { "Name", applicationName } }), application.Id, userId);

            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Domain.Application>> ApplicationsWithPermissions()
        {
            var userId = authService.CurrentUserId();

            var userApplications = unitOfWork.Repository<Domain.UserApplication, int>();
            var myApplicationsSpecification = ApplicationSpecifications.UserApplicationWithPermissions(userId, Domain.ApplicationPermission.Read);
            var applications = await userApplications.GetAsync(myApplicationsSpecification, "Application");

            return applications.Select(a => a.Application);
        }
    }
}
