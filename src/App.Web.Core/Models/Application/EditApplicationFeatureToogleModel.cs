using System;

namespace App.Web.Core.Models.Application
{
    public class EditApplicationFeatureToggleModel
    {
        public Guid FeatureToggleId { get; set; }

        public Guid EnvironmentId { get; set; }

        public string EnvironmentName { get; set; }

        public bool IsEnabled { get; set; }
    }
}
