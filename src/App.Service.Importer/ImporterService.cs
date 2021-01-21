using App.SharedKernel.Guards;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.Service.Importer
{
    public class ImporterService : IImporterService
    {
        public IEnumerable<IImporter> GetImporters()
        {
            return new List<IImporter>{
                 new JsonImporter(),
            };
        }

        public IDictionary<string, string> Parse(MemoryStream stream, string format)
        {
            var importer = GetImporters().FirstOrDefault(x => x.Name.ToLower() == format.ToLower());
            Guard.IsNotNull(importer, "Format not supported");

            return importer.Parse(stream);
        }
    }
}
