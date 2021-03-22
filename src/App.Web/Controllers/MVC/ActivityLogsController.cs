using App.Service.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class ActivityLogsController : BaseController
    {
        private readonly IActivityService activity;

        public ActivityLogsController(IActivityService activity)
        {
            this.activity = activity;
        }

        [Route("a/{applicationId}/logs")]
        public async Task<IActionResult> View([FromRoute] Guid applicationId, [FromQuery] int page = 0, [FromQuery] int pageSize = 20)
        {
            ViewData["ApplicationId"] = applicationId;

            var logs = await activity.GetLogs(applicationId, page, pageSize);
            return View(logs);
        }
    }
}