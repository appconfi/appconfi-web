using System.Collections.Generic;

namespace App.Web.Core.Models.Application
{
    public class FeatureToggleViewModel
    {
        public IEnumerable<ApplicationFeatureToggleModel> FeatureToggles { get; set; }

        public IEnumerable<ApplicationEnvironmentModel> Environments { get; set; }

        public ApplicationEnvironmentModel SelectedEnvironment { get; set; }
    }
}
