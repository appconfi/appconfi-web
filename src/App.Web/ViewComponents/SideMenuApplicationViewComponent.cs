using App.Service.Applications;
using App.Web.Core.Models.Application;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.ViewComponents
{
    public class SideMenuApplicationViewComponent : ViewComponent
    {
        private readonly IApplicationService applicationService;

        public SideMenuApplicationViewComponent(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var applications = await applicationService.ApplicationsWithPermissions();
            var model = applications.Select(a => new ApplicationModel { Id = a.Id, Name = a.Name });

            return await Task.FromResult((IViewComponentResult)View("default", model));
        }
    }
}
