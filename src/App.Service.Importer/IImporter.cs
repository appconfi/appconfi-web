using System.Collections.Generic;
using System.IO;

namespace App.Service.Importer
{
    public interface IImporter
    {
        string Name { get; }

        IDictionary<string, string> Parse(MemoryStream stream);
    }
}