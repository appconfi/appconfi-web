using App.Domain;
using App.Domain.FeatureToggles;
using App.Service.Common;
using App.Service.Importer;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class ApplicationImporter : IApplicationImporter
    {
        private readonly IHasEnvironmentPermissionPolicy hasEnvironmentPermission;
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImporterService importer;

        public ApplicationImporter(
            IHasEnvironmentPermissionPolicy hasEnvironmentPermission,
            IAuthService authService,
            IUnitOfWork unitOfWork,
            IImporterService importer)
        {
            this.hasEnvironmentPermission = hasEnvironmentPermission;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
            this.importer = importer;
        }

        public async Task ImportToggles(Guid environmentId, string format, MemoryStream stream)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasEnvironmentPermission.ToRead(userId, environmentId), "Invalid permissions for read this environment");

            var environments = unitOfWork.Repository<ApplicationEnvironment, Guid>();
            var environment = await environments.GetById(environmentId, "Application.FeatureToggles,FeatureToggleValues");
            var values = importer.Parse(stream, format);

            foreach (var entry in values)
            {
                environment.AddOrEditFeatureToggleValue(entry.Key, FeatureToggleValue.Parse(entry.Value).IsEnabled);
            }
            await unitOfWork.SaveAsync();
        }


    }
}
