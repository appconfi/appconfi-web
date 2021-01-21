using App.Service.Applications;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{

    [Authorize]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public async Task<IActionResult> View(Guid id)
        {
            var application = await applicationService.GetApplication(id);
            var model = new ApplicationModel { Id = application.Id, Name = application.Name };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New([FromForm] NewApplicationModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("index", "admin");

            Guid applicationid;
            try
            {
                applicationid = await applicationService.CreateApplication(model.Name);
            }
            catch (AppException e)
            {
                return RedirectToAction("index", "admin", new { error = e.Message });
            }

            return RedirectToAction("view", "environments", new { applicationId = applicationid });

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid applicationId, [FromForm] string applicationName)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("index", "admin");

            try
            {
                await applicationService.DeleteApplication(applicationId, applicationName);
            }
            catch (AppException e)
            {
                return RedirectToAction("index", "admin", new { error = e.Message });
            }

            return RedirectToAction("index", "admin");
        }
    }
}