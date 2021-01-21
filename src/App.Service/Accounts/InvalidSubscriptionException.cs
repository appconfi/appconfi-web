using App.SharedKernel.Exceptions;

namespace App.Service.Accounts
{
    public class InvalidSubscriptionException : AppException
    {
        public InvalidSubscriptionException(string message) : base(message)
        {

        }
    }
}
