using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IFeatureExporter
    {
        Task<IDictionary<string, object>> GetFeatures(Guid applicationId, string environmentName, string privateKey);
        Task<IDictionary<string, object>> GetFeatures(Guid applicationId, Guid environmentId);
    }
}