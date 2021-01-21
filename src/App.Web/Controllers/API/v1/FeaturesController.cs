using App.Service.Applications;
using App.Web.Core.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Web.Controllers.API.v1
{
    [Route("api/v1/features")]
    [ApiController]
    public class FeaturesController : BaseApiController
    {
        private readonly IFeatureExporter applicationExporter;
        private readonly IEnvironmentService environmentService;

        public FeaturesController(IFeatureExporter applicationExporter, IEnvironmentService environmentService)
        {
            this.applicationExporter = applicationExporter;
            this.environmentService = environmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] RequestFeaturesModel model)
        {
            return await CreateResponse(async () =>
            {
                var features = await applicationExporter.GetFeatures(model.App.Value, model.Env, model.Key);
                return Ok(features);
            });
        }

        [HttpGet("version")]
        [AllowAnonymous]
        public async Task<IActionResult> Version([FromQuery] RequestVersionModel model)
        {
            return await CreateResponse(async () =>
            {
                var output = await environmentService.GetVersion(model.App, model.Env, model.Key);
                return Ok(output);
            });

        }

    }
}