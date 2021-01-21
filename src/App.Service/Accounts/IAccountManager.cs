using System;
using System.Threading.Tasks;

namespace App.Service.Accounts
{
    public interface IAccountManager
    {
        Task RegisterUser(RegisterUserDto registerDto);

        Task<Guid> RegisterExternal(LoginExternalDto loginExternalDto);

        Task ForgotPassword(string email);

        Task ResetPassword(ResetPasswordDto resetPasswordDto);

        Task Verify(string token);
    }
}
