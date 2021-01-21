using App.Domain;
using App.Service.Applications;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{

    [Authorize]
    public class FeatureTogglesController : BaseController
    {
        private readonly IEnvironmentService environmentService;
        private readonly IFeatureToggleService featureToggleService;

        public FeatureTogglesController(
            IEnvironmentService environmentService,
            IFeatureToggleService applicationFeatureToggleService)
        {
            this.environmentService = environmentService;
            this.featureToggleService = applicationFeatureToggleService;
        }

        public async Task<IActionResult> View([FromQuery] Guid applicationId, [FromQuery] Guid? environmentId = null, [FromQuery] string search = null)
        {
            ViewData["ApplicationId"] = applicationId;

            var environments = await environmentService.GetEnvironments(applicationId);
            ApplicationEnvironment selectedEnvironment = environments.FirstOrDefault(x => x.Id == environmentId);

            if (selectedEnvironment == null)
                selectedEnvironment = environments.First(x => x.IsDefault);

            var featureToggles = await featureToggleService.GetKeyValues(selectedEnvironment.Id);

            if (!string.IsNullOrEmpty(search))
            {
                featureToggles = featureToggles.Where(f => f.Key.ToLower().Contains(search.ToLower()));
                ViewData["Search"] = search;
            }

            var model = new FeatureToggleViewModel
            {
                Environments = environments.Select(x => new ApplicationEnvironmentModel { Name = x.Name, EnvironmentId = x.Id, ApplicationId = x.ApplicationId }),
                FeatureToggles = featureToggles.Select(x => new ApplicationFeatureToggleModel { Id = x.SettingId, Key = x.Key, IsEnabled = x.IsEnabled, LastModifiedAt = x.LastModifiedAt }).OrderBy(x => x.Key),
                SelectedEnvironment = new ApplicationEnvironmentModel { ApplicationId = applicationId, EnvironmentId = selectedEnvironment.Id, Name = selectedEnvironment.Name }
            };

            return View(model);
        }

        public async Task<IActionResult> Detail([FromQuery] Guid applicationId, [FromQuery] Guid featureToggleId)
        {
            ViewData["ApplicationId"] = applicationId;

            var environments = await environmentService.GetEnvironments(applicationId);
            var featureToggle = await featureToggleService.GetFeatureToggle(applicationId, featureToggleId);
            var featureToggleValue = await featureToggleService.GetKeyValues(applicationId, featureToggleId);


            var model = new FeatureToggleDetailViewModel
            {
                Environments = environments.Select(x => new ApplicationEnvironmentModel { Name = x.Name, EnvironmentId = x.Id, ApplicationId = x.ApplicationId }),
                FeatureToggle = featureToggle,
                FeatureToggleValues = featureToggleValue
            };

            return View(model);
        }



        public async Task<IActionResult> Compare([FromQuery] Guid applicationId)
        {
            ViewData["ApplicationId"] = applicationId;

            var environments = await environmentService.GetEnvironments(applicationId);
            var dictionary = new Dictionary<string, IEnumerable<ApplicationKeyFeatureDto>>();
            foreach (var env in environments.OrderBy(x => x.Name))
            {
                var featureToggles = await featureToggleService.GetKeyValues(env.Id);
                dictionary[env.Name] = featureToggles;

            }
            var model = new CompareFeatureToggleViewModel
            {
                FeatureToggles = dictionary
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New([FromQuery] Guid applicationId, NewApplicationFeatureToggleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                    await featureToggleService.NewToggle(applicationId, model.Key, model.IsEnabled);
            }
            catch (AppException e)
            {
                return RedirectToAction("view", "featureToggles", new { error = e.Message, applicationId });
            }

            return RedirectToAction("view", "featureToggles", new { applicationId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id, [FromQueryAttribute] Guid applicationId)
        {
            await featureToggleService.RemoveFeatureToggle(id);

            return RedirectToAction("view", "featureToggles", new { applicationId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditApplicationFeatureToggleModel model, [FromQueryAttribute] Guid applicationId)
        {
            await featureToggleService.EditToggle(model.EnvironmentId, model.FeatureToggleId, model.IsEnabled);

            return RedirectToAction("view", "featureToggles", new { applicationId, environmentId = model.EnvironmentId });
        }

        [HttpPost]
        public async Task<IActionResult> EditValue([FromForm] EditApplicationFeatureToggleModel model, [FromQueryAttribute] Guid applicationId)
        {
            await featureToggleService.EditToggle(model.EnvironmentId, model.FeatureToggleId, model.IsEnabled);
            return Ok();
        }

    }
}