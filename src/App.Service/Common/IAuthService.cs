using App.Domain;
using System;
using System.Threading.Tasks;

namespace App.Service.Common
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);

        Task<LogingResult> ValidCredentials(string email, string password);

        Guid CurrentUserId();

        Task<User> CurrentUser(string include = null);
    }
}
