using System;
using System.Threading.Tasks;

namespace App.Service.Profile
{
    public interface IProfileService
    {
        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="userId">User id</param>
        Task<UserDto> GetUser(Guid userId);
    }
}
