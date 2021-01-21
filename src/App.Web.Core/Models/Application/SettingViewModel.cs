using System.Collections.Generic;

namespace App.Web.Core.Models.Application
{
    public class SettingViewModel
    {
        public IEnumerable<ApplicationSettingModel> Settings { get; set; }

        public IEnumerable<ApplicationEnvironmentModel> Environments { get; set; }

        public ApplicationEnvironmentModel SelectedEnvironment { get; set; }
    }
}
