using App.Service.Applications;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class EnvironmentsController : BaseController
    {
        private readonly IEnvironmentService environmentService;

        public EnvironmentsController(IEnvironmentService environmentService)
        {
            this.environmentService = environmentService;
        }

        [Route("a/{applicationId}/environments")]
        public async Task<IActionResult> View([FromRoute] Guid applicationId)
        {
            var envs = await environmentService.GetEnvironments(applicationId);
            ViewData["ApplicationId"] = applicationId;
            return View(envs.Select(e => new ApplicationEnvironmentModel { IsDefault = e.IsDefault, ApplicationId = e.ApplicationId, EnvironmentId = e.Id, Name = e.Name }));
        }

        [HttpPost("a/{applicationId}/environments/new")]
        public async Task<IActionResult> New([FromRoute] Guid applicationId, [FromForm] NewEnvironmentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await environmentService.CreateEnvironment(model.Name, applicationId);

                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "environments", new { applicationId, error = e.Message });
                }
            }
            return RedirectToAction("view", "environments", new { applicationId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id, [FromQuery] Guid applicationId)
        {
            await environmentService.DeleteEnvironment(id);

            return RedirectToAction("view", "environments", new { applicationId });
        }
    }
}