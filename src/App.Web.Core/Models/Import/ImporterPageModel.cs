using App.Web.Core.Models.Export;
using System.Collections.Generic;

namespace App.Web.Core.Models.Import
{
    public class ImporterPageModel
    {
        public IEnumerable<string> Importers { get; set; }
        public IEnumerable<EnvironmentModel> Enviroments { get; set; }
    }
}
