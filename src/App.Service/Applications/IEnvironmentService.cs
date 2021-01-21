using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IEnvironmentService
    {
        Task<Guid> CreateEnvironment(string name, Guid applicationId);

        Task<IEnumerable<Domain.ApplicationEnvironment>> GetEnvironments(Guid applicationId);

        Task DeleteEnvironment(Guid environmentId);

        Task<long> GetVersion(Guid applicationId, string environmentName, string privateKey);
    }
}