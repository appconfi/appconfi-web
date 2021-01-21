using System.Threading.Tasks;

namespace App.Service.Common
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
