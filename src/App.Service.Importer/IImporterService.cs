using System.Collections.Generic;
using System.IO;

namespace App.Service.Importer
{
    public interface IImporterService
    {
        IEnumerable<IImporter> GetImporters();
        IDictionary<string, string> Parse(MemoryStream stream, string format);
    }
}