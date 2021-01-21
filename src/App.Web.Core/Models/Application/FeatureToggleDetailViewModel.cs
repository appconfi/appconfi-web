using App.Domain.FeatureToggles;
using App.Service.Applications;
using System.Collections.Generic;

namespace App.Web.Core.Models.Application
{
    public class FeatureToggleDetailViewModel
    {
        public IEnumerable<ApplicationEnvironmentModel> Environments { get; set; }
        public FeatureToggle FeatureToggle { get; set; }
        public IEnumerable<ApplicationEnvKeyFeatureDto> FeatureToggleValues { get; set; }

    }
}
