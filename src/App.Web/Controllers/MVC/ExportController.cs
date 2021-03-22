using App.Service.Applications;
using App.Service.Common;
using App.Web.Core.Models.Export;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class ExportController : Controller
    {
        private readonly IUrlService urlService;
        private readonly IFeatureExporter applicationExporter;
        private readonly IEnvironmentService environmentService;

        public ExportController(
            IUrlService urlService,
            IFeatureExporter applicationExporter,
            IEnvironmentService environmentService
            )
        {
            this.urlService = urlService;
            this.applicationExporter = applicationExporter;
            this.environmentService = environmentService;
        }

        [Route("a/{applicationId}/export")]
        public async Task<IActionResult> Index([FromRoute] Guid applicationId)
        {
            ViewData["ApplicationId"] = applicationId;

            var environments = await environmentService.GetEnvironments(applicationId);
            var model = new ExporterPageModel
            {
                BaseUrl = urlService.GetBaseUrl(),
                Environments = environments.Select(x => new EnvironmentModel { Id = x.Id, Name = x.Name }),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<FileResult> DownLoad(
            [FromQuery] Guid appId,
            Guid envId,
            [FromQuery(Name = "fn")] string fileName = null)
        {
            var features = await applicationExporter.GetFeatures(appId, envId);
            return File(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(features)), "application/json", fileName ?? $"features.json");
        }
    }
}