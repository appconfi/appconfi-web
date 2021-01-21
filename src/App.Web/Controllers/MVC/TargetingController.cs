﻿using App.Domain;
using App.Service.Applications;
using App.Service.Targeting;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Targeting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class TargetingController : BaseController
    {
        private readonly IUserTargetingService userTargetingService;
        private readonly IFeatureToggleService featureToggleService;
        private readonly IEnvironmentService environmentService;

        public TargetingController(
            IUserTargetingService userTargetingService,
            IFeatureToggleService featureToggleService,
            IEnvironmentService environmentService)
        {
            this.userTargetingService = userTargetingService;
            this.featureToggleService = featureToggleService;
            this.environmentService = environmentService;
        }

        public async Task<IActionResult> View([FromQuery] Guid applicationId, [FromQuery] Guid? environmentId = null)
        {
            ViewData["ApplicationId"] = applicationId;
            var environments = await environmentService.GetEnvironments(applicationId);
            ApplicationEnvironment selectedEnvironment = environments.FirstOrDefault(x => x.Id == environmentId);
            var features = await featureToggleService.GetFeatureToggles(applicationId);

            if (selectedEnvironment == null)
                selectedEnvironment = environments.First(x => x.IsDefault);

            var targets = await userTargetingService.GetTargets(selectedEnvironment.Id);

            return View(new TargetingViewModel
            {
                Environments = environments,
                FeatureToggles = features,
                SelectedEnvironment = selectedEnvironment,
                UserTargetings = targets
            });
        }

        [HttpPost]
        public async Task<IActionResult> NewPercent([FromQuery] Guid applicationId, [FromForm] NewTargetingPercentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userTargetingService.CreatePerPercent(applicationId, model.EnvironmentId, model.FeatureToggleId, model.Percent);
                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "targeting", new { applicationId, error = e.Message });
                }
            }
            var error = string.Join(" | ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
            return RedirectToAction("view", "targeting", new { applicationId, model.EnvironmentId, error });
        }

        [HttpPost]
        public async Task<IActionResult> NewSpecific([FromQuery] Guid applicationId, [FromForm] NewTargetingSpecifiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userTargetingService.CreatePerUser(applicationId, model.EnvironmentId, model.FeatureToggleId, model.Property, model.List, model.TargetOption);
                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "targeting", new { applicationId, error = e.Message });
                }
            }
            var error = string.Join(" | ", ModelState.Values
                                          .SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage));
            return RedirectToAction("view", "targeting", new { applicationId, model.EnvironmentId, error });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id, [FromQueryAttribute] Guid applicationId)
        {
            await userTargetingService.Delete(id);
            return RedirectToAction("view", "targeting", new { applicationId });
        }
    }
}