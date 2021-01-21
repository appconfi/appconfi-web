using App.Domain;
using App.Domain.FeatureToggles;
using System.Collections.Generic;

namespace App.Web.Core.Models.Targeting
{
    public class TargetingViewModel
    {
        public IEnumerable<FeatureToggle> FeatureToggles { get; set; }
        public IEnumerable<ApplicationEnvironment> Environments { get; set; }
        public IEnumerable<UserTargeting> UserTargetings { get; set; }
        public ApplicationEnvironment SelectedEnvironment { get; set; }
    }
}
