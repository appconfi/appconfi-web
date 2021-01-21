using System.Collections.Generic;

namespace App.Web.Core.Models.Export
{
    public class ExporterPageModel
    {
        public string BaseUrl { get; set; }
        public IEnumerable<EnvironmentModel> Environments { get; set; }
    }
}
