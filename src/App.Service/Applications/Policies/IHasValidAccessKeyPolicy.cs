using System;
using System.Threading.Tasks;

namespace App.Service.Applications.Policies
{
    public interface IHasValidAccessKeyPolicy
    {
        Task<bool> ToRead(Guid applicationId, string key);
    }
}