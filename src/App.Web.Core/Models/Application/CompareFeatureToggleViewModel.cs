using App.Service.Applications;
using System.Collections.Generic;

namespace App.Web.Core.Models.Application
{
    public class CompareFeatureToggleViewModel
    {
        public Dictionary<string, IEnumerable<ApplicationKeyFeatureDto>> FeatureToggles { get; set; }

    }
}
