using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IApplicationService
    {
        Task<Guid> CreateApplication(string name);

        Task<IEnumerable<Domain.Application>> ApplicationsWithPermissions();

        Task<Domain.Application> GetApplication(Guid id);

        Task DeleteApplication(Guid id, string applicationName);
    }
}