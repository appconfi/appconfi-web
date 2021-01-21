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

        public async Task<IActionResult> View([FromQuery] Guid applicationId)
        {
            var envs = await environmentService.GetEnvironments(applicationId);
            ViewData["ApplicationId"] = applicationId;
            return View(envs.Select(e => new ApplicationEnvironmentModel { IsDefault = e.IsDefault, ApplicationId = e.ApplicationId, EnvironmentId = e.Id, Name = e.Name }));
        }

        [HttpPost]
        public async Task<IActionResult> New([FromQuery] Guid applicationId, [FromForm] NewEnvironmentModel model)
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
        public async Task<IActionResult> Delete([FromForm] Guid id, [FromQueryAttribute] Guid applicationId)
        {
            await environmentService.DeleteEnvironment(id);

            return RedirectToAction("view", "environments", new { applicationId });
        }
    }
}