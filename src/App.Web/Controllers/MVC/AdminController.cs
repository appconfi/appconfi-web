using App.Service.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IApplicationService applicationService;

        public AdminController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }
        public async Task<IActionResult> Index()
        {
            var applications = await applicationService.ApplicationsWithPermissions();
            return View(applications);
        }
    }
}