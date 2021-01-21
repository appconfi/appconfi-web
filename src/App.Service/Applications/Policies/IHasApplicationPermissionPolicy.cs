using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IHasApplicationPermissionPolicy
    {
        Task<bool> ToRead(Guid userId, Guid applicationId);

        Task<bool> ToWrite(Guid userId, Guid applicationId);

        Task<bool> ToDelete(Guid userId, Guid applicationId);
    }
}