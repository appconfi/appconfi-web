using App.Domain;
using System;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IAccessKeysService
    {
        Task<ApplicationAccessKey> GetKey(Guid applicationId);
        Task<ApplicationAccessKey> RegenerateKey(Guid applicationId);
    }
}