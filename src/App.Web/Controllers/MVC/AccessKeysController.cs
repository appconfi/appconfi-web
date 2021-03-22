using App.Service.Applications;
using App.Web.Core.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class AccessKeysController : BaseController
    {
        private readonly IAccessKeysService accessKeysService;

        public AccessKeysController(IAccessKeysService accessKeysService)
        {
            this.accessKeysService = accessKeysService;
        }

        [Route("a/{applicationId}/keys")]
        public async Task<IActionResult> View([FromRoute] Guid applicationId)
        {
            var key = await accessKeysService.GetKey(applicationId);
            ViewData["ApplicationId"] = applicationId;

            return View(new AccessKeyModel { CreatedAt = key.CreatedAt, Secret = key.Secret });
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromQuery] Guid applicationId)
        {
            await accessKeysService.RegenerateKey(applicationId);
            return RedirectToAction("view", "accesskeys", new { applicationId });
        }
    }
}