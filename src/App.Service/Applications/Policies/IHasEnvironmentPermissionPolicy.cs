using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IHasEnvironmentPermissionPolicy
    {
        Task<bool> ToRead(Guid userId, Guid environmentId);
        Task<bool> ToWrite(Guid userId, Guid environmentId);
    }
}