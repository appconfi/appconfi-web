using System;
using System.IO;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IApplicationImporter
    {
        Task ImportToggles(Guid environmentId, string format, MemoryStream stream);
    }
}