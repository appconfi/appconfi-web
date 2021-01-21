using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.Service.Importer
{
    public class JsonImporter : IImporter
    {
        public string Name => "JSON";

        public IDictionary<string, string> Parse(MemoryStream stream)
        {
            var json = Encoding.ASCII.GetString(stream.ToArray());
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}