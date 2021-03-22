using App.Service.Applications;
using App.Service.Importer;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Export;
using App.Web.Core.Models.Import;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class ImportController : Controller
    {
        private readonly IApplicationImporter applicationImporter;
        private readonly IImporterService importerService;
        private readonly IEnvironmentService environmentService;

        public ImportController(
            IApplicationImporter applicationImporter,
            IImporterService importerService,
            IEnvironmentService environmentService)
        {
            this.applicationImporter = applicationImporter;
            this.importerService = importerService;
            this.environmentService = environmentService;
        }

        [Route("a/{applicationId}/import")]
        public async Task<IActionResult> Index([FromRoute] Guid applicationId)
        {
            ViewData["ApplicationId"] = applicationId;

            var enviroments = await environmentService.GetEnvironments(applicationId);
            var importers = importerService.GetImporters();
            var model = new ImporterPageModel
            {
                Enviroments = enviroments.Select(x => new EnvironmentModel { Id = x.Id, Name = x.Name }),
                Importers = importers.Select(x => x.Name)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Post(ImportModel model)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (model.File == null)
                        throw new BadRequestException("Invalid file");

                    await model.File.CopyToAsync(memoryStream);
                    await applicationImporter.ImportToggles(model.EnvironmentId, model.Format, memoryStream);
                    return RedirectToAction("view", "featureToggles", new { applicationId = model.ApplicationId, environmentId = model.EnvironmentId });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { error = e.Message, applicationId = model.ApplicationId });
            }
        }
    }
}